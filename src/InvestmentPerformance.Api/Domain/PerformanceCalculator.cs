namespace InvestmentPerformance.Api.Domain;

public sealed record InvestmentSummary(int Id, string Name);

public sealed record InvestmentDetails(
    int Id,
    string Name,
    string AssetType,
    decimal Shares,
    decimal CostBasisPerShare,
    decimal CurrentValue,
    decimal CurrentPrice,
    string Term,
    decimal TotalGainLoss,
    string Currency,
    DateOnly PurchasedOn,
    DateOnly ValuationDate,
    DateTimeOffset PriceAsOf);

public static class PerformanceCalculator
{
    public static InvestmentDetails Calculate(Investment investment, DateOnly valuationDate)
    {
        // Bound inputs before multiplication so invalid stored data cannot overflow.
        if (investment.Shares is <= 0 or > 1_000_000_000m ||
            investment.CostBasisPerShare is < 0 or > 1_000_000_000m ||
            investment.CurrentPrice is < 0 or > 1_000_000_000m ||
            investment.PurchasedOn > valuationDate ||
            DateOnly.FromDateTime(investment.PriceAsOf.UtcDateTime) > valuationDate)
        {
            throw new InvalidOperationException("The investment contains invalid valuation data.");
        }

        var currentValue = investment.Shares * investment.CurrentPrice;
        var totalCost = investment.Shares * investment.CostBasisPerShare;
        // AddYears defines February 29's anniversary as February 28 in a non-leap year.
        var term = valuationDate > investment.PurchasedOn.AddYears(1) ? "long" : "short";

        return new InvestmentDetails(
            investment.Id, investment.Name, investment.AssetType, investment.Shares,
            investment.CostBasisPerShare, RoundMoney(currentValue), investment.CurrentPrice,
            term, RoundMoney(currentValue - totalCost), "USD", investment.PurchasedOn,
            valuationDate, investment.PriceAsOf);
    }

    private static decimal RoundMoney(decimal value) => decimal.Round(value, 2, MidpointRounding.ToEven);
}
