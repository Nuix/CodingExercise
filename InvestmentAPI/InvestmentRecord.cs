using System;

namespace InvestmentAPI
{
    public class InvestmentRecord
    {
        public long NumberOfShares { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal CurrentValue => NumberOfShares * CurrentPrice;
        private DateTime DatePurchased { get; set; } = DateTime.UtcNow;
        private TimeSpan durationOwned => DateTime.UtcNow - DatePurchased;
        public Term Term => durationOwned.TotalDays > 365.25 ? Term.Long : Term.Short;
        private decimal InitialValue => NumberOfShares * CostBasisPerShare;
        public decimal TotalGainLoss => CurrentValue - InitialValue;
        public InvestmentRecord() { }
        public InvestmentRecord(long numberOfShares, decimal costBasisPerShare, decimal currentPrice, DateTime datePurchased)
        {
            NumberOfShares = numberOfShares;
            CostBasisPerShare = costBasisPerShare;
            CurrentPrice = currentPrice;
            DatePurchased = datePurchased;
        }
    }
}
