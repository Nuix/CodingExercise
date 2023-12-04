using InvestmentPerformanceApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InvestmentPerformanceApi.Data
{
    public class InvestmentPerformanceContext : DbContext
    {
        public InvestmentPerformanceContext(DbContextOptions<InvestmentPerformanceContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Investment> Investments { get; set; }
    }
}
