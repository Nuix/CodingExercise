using InvestmentPerformanceWebApi.Models;

namespace InvestmentPerformanceWebApi.Data;

public class InvestmentContext
{
    public List<Investment> Investments { get; } = [];
}
