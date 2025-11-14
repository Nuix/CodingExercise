using InvestmentPerformance.Api.Controllers;
using InvestmentPerformance.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InvestmentPerformance.Tests;

public class InvestmentApiTests
{
    private const int ValidUserId = 101;
    private const int ValidInvestmentId = 1;
    private const int InvalidInvestmentId = -1;
    
    [Fact]
    public void GetUserInvestments_GivenValidUser_ReturnsOk()
    {
        var investmentService = new Mock<InvestmentService>();
        var logger = new Mock<ILogger<InvestmentController>>();
        var investmentController = new InvestmentController(investmentService.Object, logger.Object);
        
        var result = investmentController.GetUserInvestments(ValidUserId.ToString());
        
        Assert.IsType<OkObjectResult>(result.Result);
        
    }

    [Fact]
    public void GetInvestment_GivenValidInvestment_ReturnsOk()
    {
        var investmentService = new Mock<InvestmentService>();
        var logger = new Mock<ILogger<InvestmentController>>();
        var investmentController = new InvestmentController(investmentService.Object, logger.Object);
        
        var result = investmentController.GetInvestment(ValidInvestmentId.ToString());
        
        Assert.IsType<OkObjectResult>(result);
        
    }

    [Fact]
    public void GetInvestment_GivenInvalidInvestment_ReturnsNotFound()
    {
        var investmentService = new Mock<InvestmentService>();
        var logger = new Mock<ILogger<InvestmentController>>();
        var investmentController = new InvestmentController(investmentService.Object, logger.Object);
        
        var result = investmentController.GetInvestment(InvalidInvestmentId.ToString());
        
        Assert.IsType<NotFoundResult>(result);
    }
    
}