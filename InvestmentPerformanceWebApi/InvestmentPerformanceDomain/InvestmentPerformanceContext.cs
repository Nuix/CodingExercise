using InvestmentPerformanceDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceDomain;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class InvestmentPerformanceContext : DbContext
{
    public InvestmentPerformanceContext(DbContextOptions<InvestmentPerformanceContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HoldingEntityTypeConfguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfguration());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Holding> Holdings { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
