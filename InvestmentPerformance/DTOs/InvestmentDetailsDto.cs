namespace InvestmentPerformance.DTOs
{
    public class InvestmentDetailsDto
    {
        public int ID { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal CurrentPrice { get; set; }
        public required string Term { get; set; } // "Short-Term" or "Long-Term"
        public decimal TotalGainOrLoss { get; set; }

    }
}
