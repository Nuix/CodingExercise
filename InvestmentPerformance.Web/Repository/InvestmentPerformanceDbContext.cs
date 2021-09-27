using System;
using System.Collections.Generic;
using InvestmentPerformance.Web.Enums;
using InvestmentPerformance.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestmentPerformance.Web.Repository
{
    public class InvestmentPerformanceDbContext : DbContext
    {
        public InvestmentPerformanceDbContext(DbContextOptions<InvestmentPerformanceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInvestments>()
                .HasKey(ui => new {ui.UserId, ui.InvestmentId});

            modelBuilder.Entity<UserInvestments>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.Investments)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<UserInvestments>()
                .HasOne(ui => ui.Investment)
                .WithMany(i => i.Users)
                .HasForeignKey(bc => bc.InvestmentId);
            
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Id);
            
            modelBuilder.Entity<Investment>()
                .HasIndex(investment => investment.Id);
            
            modelBuilder.Entity<Investment>()
                .HasIndex(investment => investment.Name);
            
            modelBuilder.Entity<UserInvestments>()
                .HasIndex(a => a.Id)
                .IsUnique();
            
            modelBuilder
                .Entity<Investment>()
                .Property(d => d.InvestmentType)
                .HasConversion(new EnumToStringConverter<InvestmentType>());
            
            modelBuilder
                .Entity<UserInvestments>()
                .Property(d => d.InvestmentStatus)
                .HasConversion(new EnumToStringConverter<InvestmentStatus>());
            
            modelBuilder
                .Entity<UserInvestments>()
                .Property(d => d.InvestmentTerm)
                .HasConversion(new EnumToStringConverter<InvestmentTerm>());
            
            var user = new User()
            {
                Id = 1,
                Name = "Ari"
            };
            
            var appleStock = new Investment()
            {
                Id = 1,
                Name = "AAPL",
                InvestmentType = InvestmentType.Stock
            };
            
            var msftStock = new Investment()
            {
                Id = 2,
                Name = "MSFT",
                InvestmentType = InvestmentType.Stock
            };
            var investments = new List<Investment>();
            
            investments.Add(appleStock);
            investments.Add(msftStock);
            
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Investment>().HasData(investments);
            
            modelBuilder.Entity<UserInvestments>(b =>
            {
                b.HasData(new UserInvestments()
                {
                    Id = 1,
                    UserId = 1,
                    InvestmentId = 1,
                    CostBasis = 20,
                    Quantity = 2,
                    InvestmentStatus = InvestmentStatus.Active
                });
                b.HasData(new UserInvestments()
                {
                    Id = 2,
                    UserId = 1,
                    InvestmentId = 2,
                    CostBasis = 20,
                    Quantity = 2,
                    CurrentPrice = 30,
                    CurrentValue = 60,
                    Gain = (60 - 40),
                    InvestmentStatus = InvestmentStatus.Inactive,
                    InvestmentTerm = InvestmentTerm.ShortTerm,
                    AcquireDateUtc = DateTimeOffset.UtcNow,
                    SellDateUtc = DateTimeOffset.UtcNow
                });
            });
            
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<UserInvestments> UserInvestments { get; set; }
    }
}