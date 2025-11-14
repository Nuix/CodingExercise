using System;
using InvestmentPerformance.Api.Services;

namespace InvestmentPerformance.Api.Models;

public class InvestmentModel(
    int id,
    int userId,
    string name,
    decimal numShares,
    decimal costPerShare,
    decimal currentPrice,
    DateTime purchaseDate)
{
    public int Id { get; set; } = id;
    public int UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public decimal NumberOfShares { get; set; } = numShares;
    public decimal CostPerShare { get; set; } = costPerShare;
    public decimal CurrentPrice { get; set; } = currentPrice;
    public DateTime PurchaseDate { get; set; } = purchaseDate;

    public InvestmentRecord ToInvestment()
    {
        var currentValue = NumberOfShares * CurrentPrice;
        
        return new InvestmentRecord(
            Id, 
            NumberOfShares, 
            CostPerShare, 
            currentValue,
            CurrentPrice, 
            DateTime.UtcNow.Date.Subtract(PurchaseDate).Days > 365 ? StockTerm.LongTerm :  StockTerm.ShortTerm,
            currentValue - CostPerShare * NumberOfShares
        );
    }
}