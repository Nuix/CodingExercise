using System;
using Microsoft.EntityFrameworkCore;
using InvestmentPerformance.Models;
using InvestmentPerformance.Database;

namespace InvestmentPerformance.Database
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "UserDb");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<InvestmentDetails> InvestmentDetails { get; set; }

    }

}

