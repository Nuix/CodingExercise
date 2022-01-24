namespace InvestmentPerformanceWebApi.DTOs;

public class InvestmentDto
{
    public const string SHORT_TERM = "Short Term";
    public const string LONG_TERM = "Long Term";

    public decimal CostBasisPerShare { get; set; }
    public decimal? CurrentValue { get; set; }
    public decimal? CurrentPrice { get; set; }
    public string Term { get; set; } = SHORT_TERM;
    public decimal? TotalGainOrLoss { get; set; }
}

