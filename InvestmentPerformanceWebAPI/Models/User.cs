namespace InvestmentPerformanceWebAPI.Models;

public class User
{
    public string? Name { get; set; }
    public required List<int> InvestmentList { get; set; }
}