namespace InvestmentApi.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Ticker { get; set; } = "";
        public decimal Units { get; set; }
        public decimal CostBasis { get; set; }
    }
}
