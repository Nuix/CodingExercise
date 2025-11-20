namespace InvestmentPerformanceWebAPI.DTOs
{
    public class InvestmentDTO
    {
        public int InvestmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Ticker { get; set; } = string.Empty;
        public decimal NumberOfShares { get; set; }
        public decimal CostBasisPerShare { get; set; }
        public decimal CurrentPrice { get; set; }

        // Current total value of the position (NumberOfShares * CurrentPrice).
        public decimal CurrentValue { get; set; }

        // Investment term classification: "ShortTerm" or "LongTerm".
        public string Term { get; set; } = string.Empty;

        // CurrentValue - (NumberOfShares * CostBasisPerShare).
        public decimal TotalGainLoss { get; set; }
    }
}
