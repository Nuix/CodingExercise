using System.Collections.Generic;
using InvestmentPerformance.Api.Models;

namespace InvestmentPerformance.Api.Services;

public interface IInvestmentService
{
    IEnumerable<UserInvestmentRecord> GetInvestmentsByUserId(int userId);
    InvestmentRecord? GetInvestmentById(int investmentId);
}

public record InvestmentRecord(
    int Id,
    decimal NumberOfShares,
    decimal CostPerShare,
    decimal CurrentValue,
    decimal CurrentPrice,
    StockTerm Term,
    decimal TotalGainLoss);
    
public record UserInvestmentRecord(
    int UserId,
    string Name
);