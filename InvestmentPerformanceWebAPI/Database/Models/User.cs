namespace InvestmentPerformanceWebAPI.Database.Models
{
    /// <summary>
    /// Represents a user of the investment platform.
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Investment> Investments { get; set; } = [];
    }
}
