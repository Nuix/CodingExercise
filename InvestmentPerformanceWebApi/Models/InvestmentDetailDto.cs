namespace InvestmentPerformanceWebApi.Models;
public record InvestmentDetailDto(
    int Id,
    int UserId,
    string Name,
    decimal NumberOfShares,
    decimal CostBasisPerShare,
    decimal CurrentPrice,
    decimal CurrentValue,
    string Term,
    decimal TotalGainLoss
);
