using InvestmentPerformance.Api.Services;
using Xunit;

namespace InvestmentPerformance.Tests;

public class InvestmentServiceTests
{
    [Fact]
    public void GetInvestmentsByUserId_GivenInvalidUserId_ReturnsNothing()
    {
        const int userId = -1;
        var investmentService = new InvestmentService();
        
        var result = investmentService.GetInvestmentsByUserId(userId);
        
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetInvestments_GivenInvalidInvestmentId_ReturnsNull()
    {
        const int investmentId = -1;
        var investmentService = new InvestmentService();
        
        var result = investmentService.GetInvestmentById(investmentId);
        
        Assert.Null(result);
    }

}