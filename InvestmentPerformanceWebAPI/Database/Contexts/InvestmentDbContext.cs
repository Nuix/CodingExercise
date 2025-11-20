using InvestmentPerformanceWebAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceWebAPI.Database.Contexts
{
    public class InvestmentDbContext(DbContextOptions<InvestmentDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Investment> Investments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Investments)
                .WithOne(i => i.User!)
                .HasForeignKey(i => i.UserId);


            // manually create some data entities
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Daniel Rivers" },
                new User { UserId = 2, Name = "Sophia Bennett" },
                new User { UserId = 3, Name = "Marcus Holt" },
                new User { UserId = 4, Name = "Emma Caldwell" },
                new User { UserId = 5, Name = "Jonathan Pierce" }
            );


            modelBuilder.Entity<Investment>().HasData(
                new Investment
                {
                    InvestmentId = 1,
                    UserId = 1,
                    Name = "Berkshire Hathaway Inc.",
                    Ticker = "BRK.B",
                    NumberOfShares = 4,
                    CostBasisPerShare = 290.75m,
                    CurrentPrice = 365.40m,
                    TimeOfPurchase = new DateTime(2022, 4, 12)
                },
                new Investment
                {
                    InvestmentId = 2,
                    UserId = 1,
                    Name = "Procter & Gamble Co.",
                    Ticker = "PG",
                    NumberOfShares = 18,
                    CostBasisPerShare = 130.10m,
                    CurrentPrice = 153.25m,
                    TimeOfPurchase = new DateTime(2023, 11, 3)
                },
                new Investment
                {
                    InvestmentId = 3,
                    UserId = 2,
                    Name = "Costco Wholesale Corporation",
                    Ticker = "COST",
                    NumberOfShares = 6,
                    CostBasisPerShare = 470.20m,
                    CurrentPrice = 680.50m,
                    TimeOfPurchase = new DateTime(2021, 8, 19)
                },
                new Investment
                {
                    InvestmentId = 4,
                    UserId = 2,
                    Name = "Walt Disney Company",
                    Ticker = "DIS",
                    NumberOfShares = 22,
                    CostBasisPerShare = 92.15m,
                    CurrentPrice = 101.85m,
                    TimeOfPurchase = new DateTime(2024, 1, 7)
                },
                new Investment
                {
                    InvestmentId = 5,
                    UserId = 3,
                    Name = "Intel Corporation",
                    Ticker = "INTC",
                    NumberOfShares = 30,
                    CostBasisPerShare = 28.00m,
                    CurrentPrice = 36.75m,
                    TimeOfPurchase = new DateTime(2023, 5, 16)
                },
                new Investment
                {
                    InvestmentId = 6,
                    UserId = 3,
                    Name = "Chevron Corporation",
                    Ticker = "CVX",
                    NumberOfShares = 12,
                    CostBasisPerShare = 148.50m,
                    CurrentPrice = 159.95m,
                    TimeOfPurchase = new DateTime(2022, 2, 27)
                },
                new Investment
                {
                    InvestmentId = 7,
                    UserId = 4,
                    Name = "UnitedHealth Group Incorporated",
                    Ticker = "UNH",
                    NumberOfShares = 3,
                    CostBasisPerShare = 460.00m,
                    CurrentPrice = 529.40m,
                    TimeOfPurchase = new DateTime(2023, 8, 24)
                },
                new Investment
                {
                    InvestmentId = 8,
                    UserId = 4,
                    Name = "PepsiCo, Inc.",
                    Ticker = "PEP",
                    NumberOfShares = 14,
                    CostBasisPerShare = 165.25m,
                    CurrentPrice = 172.45m,
                    TimeOfPurchase = new DateTime(2021, 12, 10)
                },
                new Investment
                {
                    InvestmentId = 9,
                    UserId = 5,
                    Name = "AbbVie Inc.",
                    Ticker = "ABBV",
                    NumberOfShares = 9,
                    CostBasisPerShare = 112.80m,
                    CurrentPrice = 157.60m,
                    TimeOfPurchase = new DateTime(2024, 3, 5)
                },
                new Investment
                {
                    InvestmentId = 10,
                    UserId = 5,
                    Name = "Ford Motor Company",
                    Ticker = "F",
                    NumberOfShares = 40,
                    CostBasisPerShare = 11.45m,
                    CurrentPrice = 13.90m,
                    TimeOfPurchase = new DateTime(2022, 9, 22)
                }
            );
        }
    }
}
