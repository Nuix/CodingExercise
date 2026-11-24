using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using InvestmentPerformance.Api.Data;
using InvestmentPerformance.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Api.Tests;

public sealed class InvestmentApiTests
{
    [Fact]
    public async Task ListsOnlyRequestedUsersInvestments()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        var first = await client.GetFromJsonAsync<InvestmentSummary[]>("/api/users/1/investments");
        var second = await client.GetFromJsonAsync<InvestmentSummary[]>("/api/users/2/investments");
        Assert.NotNull(first);
        Assert.Equal([1, 2, 3], first.Select(x => x.Id));
        Assert.All(first, investment => Assert.False(string.IsNullOrWhiteSpace(investment.Name)));
        Assert.Equal(4, Assert.Single(second!).Id);
    }

    [Fact]
    public async Task KnownUserWithoutInvestmentsGetsEmptyList()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        Assert.Empty((await client.GetFromJsonAsync<InvestmentSummary[]>("/api/users/3/investments"))!);
    }

    [Fact]
    public async Task DetailsContainAllMetricsAndSnapshotDates()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        var result = await client.GetFromJsonAsync<InvestmentDetails>("/api/users/1/investments/1");
        Assert.NotNull(result);
        Assert.Equal(12.5m, result.Shares);
        Assert.Equal(80m, result.CostBasisPerShare);
        Assert.Equal(92m, result.CurrentPrice);
        Assert.Equal(1150m, result.CurrentValue);
        Assert.Equal(150m, result.TotalGainLoss);
        Assert.Equal("long", result.Term);
        Assert.Equal("USD", result.Currency);
        Assert.Equal(new DateOnly(2026, 11, 23), result.ValuationDate);
        Assert.Equal(new DateTimeOffset(2026, 11, 20, 21, 0, 0, TimeSpan.Zero), result.PriceAsOf);
    }

    [Theory]
    [InlineData("/api/users/999/investments")]
    [InlineData("/api/users/1/investments/999")]
    [InlineData("/api/users/1/investments/4")]
    [InlineData("/api/users/999/investments/1")]
    public async Task MissingOrOtherUsersResourceReturnsProblemDetails(string path)
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        var response = await client.GetAsync(path);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType!.MediaType);
        var problem = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal(404, problem.GetProperty("status").GetInt32());
        Assert.False(string.IsNullOrWhiteSpace(problem.GetProperty("traceId").GetString()));
    }

    [Theory]
    [InlineData("/api/users/abc/investments")]
    [InlineData("/api/users/0/investments")]
    [InlineData("/api/users/-1/investments")]
    [InlineData("/api/users/2147483648/investments")]
    [InlineData("/api/users/1/investments/nope")]
    [InlineData("/api/users/1/investments/0")]
    public async Task InvalidIdentifiersReturnValidationProblems(string path)
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        var response = await client.GetAsync(path);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal(400, problem.GetProperty("status").GetInt32());
        Assert.True(problem.TryGetProperty("errors", out _));
    }

    [Fact]
    public async Task SQLitePreservesExactFractionalAmountsThroughHttpResponse()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        const decimal shares = 0.123456789012345678m;
        const decimal cost = 40.123456789012345678m;
        const decimal price = 41.987654321098765432m;
        await factory.WithDatabaseAsync(async db =>
        {
            var holding = await db.Investments.SingleAsync(x => x.Id == 1);
            holding.Shares = shares;
            holding.CostBasisPerShare = cost;
            holding.CurrentPrice = price;
            await db.SaveChangesAsync();
        });

        var result = await client.GetFromJsonAsync<InvestmentDetails>("/api/users/1/investments/1");
        Assert.Equal(shares, result!.Shares);
        Assert.Equal(cost, result.CostBasisPerShare);
        Assert.Equal(price, result.CurrentPrice);
        Assert.Equal(decimal.Round(shares * price, 2, MidpointRounding.ToEven), result.CurrentValue);
        Assert.Equal(decimal.Round(shares * price - shares * cost, 2, MidpointRounding.ToEven), result.TotalGainLoss);
    }

    [Fact]
    public async Task UnexpectedErrorsReturnGenericProblemWithoutStackTrace()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        await factory.WithDatabaseAsync(async db =>
        {
            var holding = await db.Investments.SingleAsync(x => x.Id == 1);
            holding.Shares = -1;
            await db.SaveChangesAsync();
        });
        var response = await client.GetAsync("/api/users/1/investments/1");
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        using var json = JsonDocument.Parse(body);
        Assert.Equal("An unexpected error occurred.", json.RootElement.GetProperty("title").GetString());
        Assert.True(json.RootElement.TryGetProperty("traceId", out _));
        Assert.DoesNotContain("InvalidOperationException", body);
        Assert.DoesNotContain("PerformanceCalculator", body);
    }

    [Fact]
    public async Task ReinitializingDatabasePreservesExistingDataAndDoesNotDuplicateSeeds()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        await factory.WithDatabaseAsync(async db =>
        {
            var holding = await db.Investments.SingleAsync(x => x.Id == 1);
            holding.CurrentPrice = 101.125m;
            await db.SaveChangesAsync();
            await DemoData.InitializeAsync(db, CancellationToken.None);
        });
        await factory.WithDatabaseAsync(async db =>
        {
            Assert.Equal(3, await db.Users.CountAsync());
            Assert.Equal(4, await db.Investments.CountAsync());
            Assert.Equal(101.125m, (await db.Investments.SingleAsync(x => x.Id == 1)).CurrentPrice);
        });
    }

    [Theory]
    [InlineData("/health")]
    [InlineData("/docs")]
    [InlineData("/openapi/v1.json")]
    public async Task HealthAndDocumentationAreAvailable(string path)
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync(path)).StatusCode);
    }
}
