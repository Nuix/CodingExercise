namespace InvestmentPerformanceWebAPI.Database.Models
{
    /// <summary>
    /// Represents a single investment position in a user's portfolio.
    /// </summary>
    public class Investment
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Ticker { get; set; } = string.Empty;

        public decimal NumberOfShares { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime TimeOfPurchase { get; set; }

        public User? User { get; set; }
    }
}
