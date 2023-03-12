using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Persistence
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<StockInvestment> StockInvestments { get;set; }

        public DbSet<InvestmentType> InvestmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockInvestment>().HasKey(x => new { x.InvestmentId, x.StockId });
        }
    }
}
