using NUIX.InvestmentPerformance.API.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace NUIX.InvestmentPerformance.API.Data
{
    public class InvestmentDbContext : DbContext
    {
        public InvestmentDbContext()
        {

        }

        public InvestmentDbContext(DbContextOptions<InvestmentDbContext> options)
        : base(options) { }

        public DbSet<UserInvestment> Users => Set<UserInvestment>();
        public DbSet<Investment> Investments => Set<Investment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<UserInvestment>()
                .HasMany(u => u.UserInvestments)
                .WithOne()
                .HasForeignKey(i => i.InvestmentID);
        }
    }
}
