namespace InvestmentApi.Models
{
    public class CreateInvestmentDto
    {
        public string Ticker { get; set; } = "";
        public decimal Units { get; set; }
        public decimal CostBasis { get; set; }
    }
}
