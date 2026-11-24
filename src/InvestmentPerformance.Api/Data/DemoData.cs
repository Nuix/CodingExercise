using InvestmentPerformance.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Api.Data;

public static class DemoData
{
    public static async Task InitializeAsync(InvestmentsDbContext db, CancellationToken cancellationToken)
    {
        await db.Database.MigrateAsync(cancellationToken);
        if (await db.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var priceAsOf = new DateTimeOffset(2026, 11, 20, 21, 0, 0, TimeSpan.Zero);
        db.Users.AddRange(
            new User { Id = 1, Name = "Demo Investor One" },
            new User { Id = 2, Name = "Demo Investor Two" },
            new User { Id = 3, Name = "Demo Investor Without Holdings" });
        db.Investments.AddRange(
            new Investment { Id = 1, UserId = 1, Name = "Example Technology Stock", AssetType = "stock",
                Shares = 12.5m, CostBasisPerShare = 80m, CurrentPrice = 92m,
                PurchasedOn = new DateOnly(2024, 5, 10), PriceAsOf = priceAsOf },
            new Investment { Id = 2, UserId = 1, Name = "Example Corporate Bond", AssetType = "bond",
                Shares = 8m, CostBasisPerShare = 100m, CurrentPrice = 90m,
                PurchasedOn = new DateOnly(2026, 6, 15), PriceAsOf = priceAsOf },
            new Investment { Id = 3, UserId = 1, Name = "Example Balanced Mutual Fund", AssetType = "mutualFund",
                Shares = 3.125m, CostBasisPerShare = 40.125m, CurrentPrice = 40.125m,
                PurchasedOn = new DateOnly(2026, 10, 1), PriceAsOf = priceAsOf },
            new Investment { Id = 4, UserId = 2, Name = "Example Income Stock", AssetType = "stock",
                Shares = 10m, CostBasisPerShare = 50m, CurrentPrice = 55m,
                PurchasedOn = new DateOnly(2025, 2, 1), PriceAsOf = priceAsOf });
        await db.SaveChangesAsync(cancellationToken);
    }
}
