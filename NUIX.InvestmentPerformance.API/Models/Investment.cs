using System.ComponentModel.DataAnnotations.Schema;

namespace NUIX.InvestmentPerformance.API.Models
{
    public class Investment
    {
        public Guid InvestmentID { get; set; }
        public Guid UserID { get; set; }
        public string InvestmentName { get; set; }
        public int NumberOfShares { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public decimal CurrentPricePerShare { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
