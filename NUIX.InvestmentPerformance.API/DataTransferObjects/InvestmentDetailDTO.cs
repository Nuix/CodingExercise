using NUIX.InvestmentPerformance.API.Models.Enums;

namespace NUIX.InvestmentPerformance.API.DataTransferObjects
{
    public class InvestmentDetailDTO
    {
        public Guid InvestmentID { get; set; }
        public string InvestmentName { get; set; }
        public int NumberOfShares { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal CurrentValue { get; set; }
        public InvestmentTerm Term { get; set; }
        public decimal TotalGainOrLoss { get; set; }
    }
}
