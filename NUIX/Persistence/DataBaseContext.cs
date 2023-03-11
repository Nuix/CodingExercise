using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<StockInvestment> StockInvestments { get;set; }
    }
}
