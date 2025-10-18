using InvestmentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentApi.Data
{
    // Fix: Inherit from DbContext to use Entity Framework Core features
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Investment> Investments => Set<Investment>();
    }
}
