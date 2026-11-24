using System.Globalization;
using InvestmentPerformance.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestmentPerformance.Api.Data;

public sealed class InvestmentsDbContext(DbContextOptions<InvestmentsDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Investment> Investments => Set<Investment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(x => x.Name).HasMaxLength(100);
        var investment = modelBuilder.Entity<Investment>();
        investment.Property(x => x.Name).HasMaxLength(200);
        investment.Property(x => x.AssetType).HasMaxLength(20);
        investment.HasOne<User>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        investment.HasIndex(x => new { x.UserId, x.Id });

        // Exact decimal text avoids SQLite REAL's binary floating-point rounding.
        // Monetary calculations happen in C# after retrieval, never in SQLite SQL.
        var exactDecimal = new ValueConverter<decimal, string>(
            value => value.ToString(CultureInfo.InvariantCulture),
            value => decimal.Parse(value, CultureInfo.InvariantCulture));
        investment.Property(x => x.Shares).HasConversion(exactDecimal).HasColumnType("TEXT");
        investment.Property(x => x.CostBasisPerShare).HasConversion(exactDecimal).HasColumnType("TEXT");
        investment.Property(x => x.CurrentPrice).HasConversion(exactDecimal).HasColumnType("TEXT");
    }
}
