using Microsoft.EntityFrameworkCore;
using Nuix.InvestmentPerformance.Data.Sqlite.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Sqlite
{
    public class InvestmentPerformanceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public InvestmentPerformanceContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>()
                .HasKey(p => new { p.Date, p.Symbol });

            modelBuilder.Entity<Stock>()
                .HasKey(s => new { s.Symbol });
        }
    }

}
