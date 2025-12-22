

namespace InvestmentPerformanceWebApi.Domain.Models
{
    public class Investment
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public int Shares { get; private set; }
        public decimal CostBasis { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public DateTime PurchasedUtc { get; private set; }

        public decimal CurrentValue => Shares * CurrentPrice;

        public string Term => GetTerm(DateTime.UtcNow);
        public decimal TotalGainOrLoss => Shares * CurrentPrice - Shares * CostBasis;


        private string GetTerm(DateTime asOf)
        {
            return (asOf - PurchasedUtc).TotalDays >= 365
                ? "Long Term"
                : "Short Term";
        }

        public Investment(
            string name,
            int id,
            int shares,
            decimal costBasis,
            decimal currentPrice,
            DateTime purchasedDateUtc)
        {
            if (shares < 0)
            {
                throw new ArgumentException("Shares must be greater than 0");
            }
            Name = name;
            Id = id;
            Shares = shares;
            CostBasis = costBasis;
            CurrentPrice = currentPrice;
            PurchasedUtc = purchasedDateUtc;
        }
    }
}
