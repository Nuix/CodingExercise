namespace InvestmentPerformanceWebApi.Models;

public class Investment
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal NumberOfShares { get; init; }
    public decimal CostBasisPerShare { get; init; }
    public decimal CurrentPrice { get; init; }
    public DateTime PurchaseDate { get; init; }
    
    public decimal CurrentValue => NumberOfShares * CurrentPrice;
    public string Term => (DateTime.Now - PurchaseDate).TotalDays > 365 ? "Long Term" : "Short Term";
    public decimal TotalGainLoss => CurrentValue - NumberOfShares * CostBasisPerShare;
}