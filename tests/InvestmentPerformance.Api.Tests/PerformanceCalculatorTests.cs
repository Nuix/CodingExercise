using InvestmentPerformance.Api.Domain;

namespace InvestmentPerformance.Api.Tests;

public sealed class PerformanceCalculatorTests
{
    [Theory]
    [InlineData("12.5", "80", "92", "1150", "150")]
    [InlineData("8", "100", "90", "720", "-80")]
    [InlineData("3.125", "40.125", "40.125", "125.39", "0")]
    [InlineData("0.1", "0.1", "0.2", "0.02", "0.01")]
    [InlineData("1", "1", "0", "0", "-1")]
    public void CalculatesValueAndUnrealizedGainExactly(string shares, string cost, string price, string value, string gain)
    {
        var holding = Holding();
        holding.Shares = Parse(shares);
        holding.CostBasisPerShare = Parse(cost);
        holding.CurrentPrice = Parse(price);

        var result = PerformanceCalculator.Calculate(holding, new DateOnly(2026, 11, 23));

        Assert.Equal(Parse(value), result.CurrentValue);
        Assert.Equal(Parse(gain), result.TotalGainLoss);
        Assert.Equal(holding.Shares, result.Shares);
        Assert.Equal(holding.CostBasisPerShare, result.CostBasisPerShare);
        Assert.Equal(holding.CurrentPrice, result.CurrentPrice);
        Assert.Equal("USD", result.Currency);
    }

    [Theory]
    [InlineData("2025-11-23", "2026-11-22", "short")]
    [InlineData("2025-11-23", "2026-11-23", "short")]
    [InlineData("2025-11-23", "2026-11-24", "long")]
    [InlineData("2024-02-29", "2025-02-28", "short")]
    [InlineData("2024-02-29", "2025-03-01", "long")]
    [InlineData("2023-03-01", "2024-02-29", "short")]
    [InlineData("2023-03-01", "2024-03-01", "short")]
    public void UsesCalendarAnniversaryForTerm(string purchased, string asOf, string expectedTerm)
    {
        var holding = Holding();
        holding.PurchasedOn = DateOnly.Parse(purchased);
        holding.PriceAsOf = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Assert.Equal(expectedTerm,
            PerformanceCalculator.Calculate(holding, DateOnly.Parse(asOf)).Term);
    }

    [Fact]
    public void RoundsOnlyFinalTotalsUsingMidpointToEven()
    {
        var holding = Holding();
        holding.Shares = 1m;
        holding.CostBasisPerShare = 0.005m;
        holding.CurrentPrice = 0.015m;

        var result = PerformanceCalculator.Calculate(holding, new DateOnly(2026, 11, 23));

        Assert.Equal(0.02m, result.CurrentValue);
        Assert.Equal(0.01m, result.TotalGainLoss);
        Assert.Equal(0.005m, result.CostBasisPerShare);
    }

    [Theory]
    [InlineData("0", "1", "1")]
    [InlineData("-1", "1", "1")]
    [InlineData("1", "-1", "1")]
    [InlineData("1", "1", "-1")]
    [InlineData("1000000001", "1", "1")]
    [InlineData("1", "1000000001", "1")]
    [InlineData("1", "1", "1000000001")]
    public void RejectsInvalidStoredAmounts(string shares, string cost, string price)
    {
        var holding = Holding();
        holding.Shares = Parse(shares);
        holding.CostBasisPerShare = Parse(cost);
        holding.CurrentPrice = Parse(price);
        Assert.Throws<InvalidOperationException>(() =>
            PerformanceCalculator.Calculate(holding, new DateOnly(2026, 11, 23)));
    }

    [Fact]
    public void RejectsPurchaseOrQuoteFromFuture()
    {
        var holding = Holding();
        Assert.Throws<InvalidOperationException>(() =>
            PerformanceCalculator.Calculate(holding, new DateOnly(2023, 1, 1)));
        holding.PurchasedOn = new DateOnly(2024, 1, 1);
        Assert.Throws<InvalidOperationException>(() =>
            PerformanceCalculator.Calculate(holding, new DateOnly(2025, 1, 1)));
    }

    private static decimal Parse(string value) => decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture);

    private static Investment Holding() => new()
    {
        Id = 1, UserId = 1, Name = "Test holding", AssetType = "stock",
        Shares = 12.5m, CostBasisPerShare = 80m, CurrentPrice = 92m,
        PurchasedOn = new DateOnly(2024, 5, 10),
        PriceAsOf = new DateTimeOffset(2026, 11, 20, 21, 0, 0, TimeSpan.Zero)
    };
}
