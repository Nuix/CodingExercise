using System;
using InvestmentPerformanceWebApi.Domain.Models;
using Xunit;

namespace InvestmentPerformanceWebApi.Tests.Domain
{
    public class InvestmentTests
    {
        [Fact]
        public void Constructor_WithNegativeShares_ThrowsArgumentException()
        {
            // Arrange
            string name = "Test Investment";
            int id = 1;
            int shares = -5;
            decimal costBasis = 100m;
            decimal currentPrice = 120m;
            DateTime purchasedDate = DateTime.UtcNow;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Investment(name, id, shares, costBasis, currentPrice, purchasedDate));
        }

        [Fact]
        public void CurrentValue_ComputesCorrectly()
        {
            // Arrange
            var investment = new Investment(
                "Test",
                1,
                10,
                50m,
                60m,
                DateTime.UtcNow.AddDays(-10));

            // Act
            var value = investment.CurrentValue;

            // Assert
            Assert.Equal(10 * 60m, value);
        }

        [Theory]
        [InlineData(-10, "Short Term")]  
        [InlineData(-400, "Long Term")]
        [InlineData(365, "Short Term")]
        [InlineData(1, "Short Term")]
        //[InlineData(100000000000, "Long Term")] //chokes on this, would need to adjust data rules for how long someone can hold an investment for
        public void Term_ReturnsCorrectValue(int daysOffset, string expectedTerm)
        {
            // Arrange
            var purchasedDate = DateTime.UtcNow.AddDays(daysOffset);
            var investment = new Investment(
                "Test",
                1,
                10,
                50m,
                60m,
                purchasedDate);

            // Act
            var term = investment.Term;

            // Assert
            Assert.Equal(expectedTerm, term);
        }

        [Fact]
        public void TotalGainOrLoss_ComputesCorrectly()
        {
            // Arrange
            var investment = new Investment(
                "Test",
                1,
                10,
                50m,
                60m,
                DateTime.UtcNow.AddDays(-10));

            // Act
            var totalGain = investment.TotalGainOrLoss;

            // Assert
            Assert.Equal((10 * 60m) - (10 * 50m), totalGain);
        }

        [Fact]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            string name = "Test Investment";
            int id = 1;
            int shares = 10;
            decimal costBasis = 50m;
            decimal currentPrice = 60m;
            DateTime purchasedDate = DateTime.UtcNow;

            // Act
            var investment = new Investment(name, id, shares, costBasis, currentPrice, purchasedDate);

            // Assert
            Assert.Equal(name, investment.Name);
            Assert.Equal(id, investment.Id);
            Assert.Equal(shares, investment.Shares);
            Assert.Equal(costBasis, investment.CostBasis);
            Assert.Equal(currentPrice, investment.CurrentPrice);
            Assert.Equal(purchasedDate, investment.PurchasedUtc);
        }
    }
}
