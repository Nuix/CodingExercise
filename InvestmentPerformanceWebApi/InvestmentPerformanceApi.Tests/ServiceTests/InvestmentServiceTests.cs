using InvestmentPerformanceWebApi.DbInfrastructure;
using InvestmentPerformanceWebApi.DbInfrastructure.Models;
using InvestmentPerformanceWebApi.Domain.Services;
using Microsoft.EntityFrameworkCore;


public class InvestmentServiceTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB for each test
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetInvestmentsForUserAsync_UserExists_ReturnsInvestments()
    {
        // Arrange
        var context = GetInMemoryDbContext();

        var user = new User { Id = 1, FirstName = "Alice", LastName = "Slim", UserName = "salice" };
        context.Users.Add(user);
        context.Investments.Add(new Investment
        {
            Id = 10,
            UserId = user.Id,
            Name = "Stock A",
            Shares = 5,
            CostBasis = 100,
            CurrentPrice = 120,
            PurchasedAtUtc = DateTime.UtcNow
        });
        await context.SaveChangesAsync();

        var service = new InvestmentService(context);

        // Act
        var investments = await service.GetInvestmentsForUserAsync(user.Id);

        // Assert
        Assert.Single(investments);
        Assert.Equal("Stock A", investments.First().Name);
    }

    [Fact]
    public async Task GetInvestmentsForUserAsync_UserDoesNotExist_ThrowsKeyNotFoundException()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new InvestmentService(context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetInvestmentsForUserAsync(999));
    }

    [Fact]
    public async Task GetInvestmentForUserAsync_InvestmentExists_ReturnsInvestment()
    {
        // Arrange
        var context = GetInMemoryDbContext();

        var user = new User { Id = 1, FirstName = "Alice", LastName = "Slim", UserName = "salice" };
        context.Users.Add(user);
        var investment = new Investment
        {
            Id = 10,
            UserId = user.Id,
            Name = "Stock A",
            Shares = 5,
            CostBasis = 100,
            CurrentPrice = 120,
            PurchasedAtUtc = DateTime.UtcNow
        };
        context.Investments.Add(investment);
        await context.SaveChangesAsync();

        var service = new InvestmentService(context);

        // Act
        var result = await service.GetInvestmentForUserAsync(user.Id, investment.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(investment.Id, result.Id);
    }

    [Fact]
    public async Task GetInvestmentForUserAsync_InvestmentDoesNotExist_ThrowsKeyNotFoundException()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new InvestmentService(context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetInvestmentForUserAsync(1, 999));
    }
}
