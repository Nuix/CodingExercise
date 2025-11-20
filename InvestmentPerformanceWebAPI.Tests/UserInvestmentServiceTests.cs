using InvestmentPerformanceWebAPI.Database.Contexts;
using InvestmentPerformanceWebAPI.Database.Models;
using InvestmentPerformanceWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceWebAPI.Tests
{
    public class UserInvestmentServiceTests
    {
        private static InvestmentDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<InvestmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new InvestmentDbContext(options);

            context.Investments.Add(new Investment
            {
                InvestmentId = 1,
                UserId = 1,
                Name = "Test",
                Ticker = "TST",
                NumberOfShares = 10m,
                CostBasisPerShare = 100m,
                CurrentPrice = 150m,
                TimeOfPurchase = DateTime.UtcNow.AddDays(-100)
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetInvestment_Computes_Performance_Correctly()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var service = new UserInvestmentService(context);

            // Act
            var result = await service.GetInvestmentAsync(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1500m, result!.CurrentValue);      // 10 * 150
            Assert.Equal(500m, result.TotalGainLoss);       // 1500 - 1000
            Assert.Equal("ShortTerm", result.Term);         // 100 days < 365
        }

        [Fact]
        public async Task GetInvestment_ReturnsNull_For_WrongUser()
        {
            using var context = CreateInMemoryContext();

            // Add same investment for different user
            context.Investments.Add(new Investment
            {
                InvestmentId = 2,
                UserId = 2,
                Name = "ForeignInvestment",
                Ticker = "FI",
                NumberOfShares = 10m,
                CostBasisPerShare = 20m,
                CurrentPrice = 22m,
                TimeOfPurchase = DateTime.UtcNow.AddDays(-10)
            });

            context.SaveChanges();

            var service = new UserInvestmentService(context);

            // Act - user 1 tries to access investment 2 (owned by user 2)
            var result = await service.GetInvestmentAsync(1, 2);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task GetInvestment_ReturnsNull_If_NotFound()
        {
            using var context = CreateInMemoryContext();
            var service = new UserInvestmentService(context);

            // Act
            var result = await service.GetInvestmentAsync(1, 999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetInvestment_Computes_LongTerm_Correctly()
        {
            using var context = CreateInMemoryContext();

            // Modify existing seed to be long-term (> 365 days)
            var investment = context.Investments.First();
            investment.TimeOfPurchase = DateTime.UtcNow.AddDays(-400);
            context.SaveChanges();

            var service = new UserInvestmentService(context);

            // Act
            var result = await service.GetInvestmentAsync(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("LongTerm", result!.Term);
        }


    }
}