using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Models
{
    public class InvestmentContext : DbContext
    {
        public InvestmentContext(DbContextOptions<InvestmentContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Investment> Investments { get; set; }
    }
}
