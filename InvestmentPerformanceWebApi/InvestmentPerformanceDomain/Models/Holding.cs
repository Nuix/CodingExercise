using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPerformanceDomain.Models;

public enum InvestmentType
{
    Stock,
    Bond,
    MutualFunds
}

public class Holding
{
    public int UserId { get; set; }
    public string Symbol { get; set; } = "";
    public string Name { get; set; } = "";
    public InvestmentType HoldingType { get; set; } = InvestmentType.Stock;
    public decimal Cost { get; set; } = 0;
    public decimal Amount { get; set; } = 0;
    public DateTime OpenDate { get; set; }
}

public class HoldingEntityTypeConfguration : IEntityTypeConfiguration<Holding>
{
    public void Configure(EntityTypeBuilder<Holding> builder)
    {
        builder.HasKey(p => new { p.UserId, p.Symbol });
        builder.Property(p => p.Symbol).HasMaxLength(200);
        builder.Property(p => p.Name).HasMaxLength(1000);
    }
}

