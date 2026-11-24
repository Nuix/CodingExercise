namespace InvestmentPerformance.Api.Domain;

public sealed class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

// Each investment is a user-owned purchase lot with a single purchase date and price.
public sealed class Investment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Name { get; set; }
    public required string AssetType { get; set; }
    public decimal Shares { get; set; }
    public decimal CostBasisPerShare { get; set; }
    public decimal CurrentPrice { get; set; }
    public DateOnly PurchasedOn { get; set; }
    public DateTimeOffset PriceAsOf { get; set; }
}
