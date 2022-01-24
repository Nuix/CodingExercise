using InvestmentPerformanceWebApi.Controllers;
using InvestmentPerformanceWebApi.DTOs;
using InvestmentPerformanceWebApi.Services;
using InvestmentPerformanceWebApi.Test.Spys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPerformanceWebApi.Test.Controllers;

public class HoldingControllerTest
{
    private readonly HoldingController _holdingController;
    private readonly Mock<IHoldingService> _holdingService = new Mock<IHoldingService>();
    private readonly SpyLogger<HoldingController> _spyLogger = new SpyLogger<HoldingController>();

    private readonly IList<InvestmentSummaryDto> holdingsSample = new List<InvestmentSummaryDto>()
        {
            new InvestmentSummaryDto() { Id = "IBM", Name = "INTERNATIONAL BUSINESS MACHINES CORPORATION"},
            new InvestmentSummaryDto() { Id = "TSLA", Name = "TESLA, INC."},
        };

    private readonly InvestmentDto holdingSample = new InvestmentDto
    {
        CostBasisPerShare = 100,

        CurrentValue = 222.22m,
        CurrentPrice = 111.11m,
        Term = "Short Term",
        TotalGainOrLoss = 22.22m,
    };

    public HoldingControllerTest()
    {

        _holdingController = new HoldingController(_holdingService.Object, _spyLogger);
        _holdingController.MockProblemDetailFactory();
    }

    [Fact]
    public async Task GetHoldings_OkResult()
    {
        // Arrange
        var userId = 5;
        _holdingService.Setup(h => h.UserExistsAsync(userId)).ReturnsAsync(true).Verifiable();
        _holdingService.Setup(h => h.GetHoldingsAsync(userId)).ReturnsAsync(holdingsSample).Verifiable();

        // Act
        var result = await _holdingController.GetHoldings(userId);

        // Assert
        _holdingService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(200, statusCodeResult.StatusCode);
        var returnObject = Assert.IsAssignableFrom<IList<InvestmentSummaryDto>>(statusCodeResult.Value);
        Assert.Equal(holdingsSample.Count, returnObject.Count);
        Assert.Equal(holdingsSample[0].Id, returnObject[0].Id);
        Assert.Equal(holdingsSample[0].Name, returnObject[0].Name);
        Assert.Equal(holdingsSample[1].Id, returnObject[1].Id);
        Assert.Equal(holdingsSample[1].Name, returnObject[1].Name);
        Assert.False(_spyLogger.LoggerWasCalled);
    }

    [Fact]
    public async Task GetHoldings_OkEmptyResult()
    {
        // Arrange
        var userId = 5;
        _holdingService.Setup(h => h.UserExistsAsync(userId)).ReturnsAsync(true).Verifiable();
        _holdingService.Setup(h => h.GetHoldingsAsync(userId)).ReturnsAsync(new List<InvestmentSummaryDto>()).Verifiable();

        // Act
        var result = await _holdingController.GetHoldings(userId);

        // Assert
        _holdingService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(200, statusCodeResult.StatusCode);
        var returnObject = Assert.IsAssignableFrom<IList<InvestmentSummaryDto>>(statusCodeResult.Value);
        Assert.Empty(returnObject);
    }

    [Fact]
    public async Task GetHoldings_NotFoundResult()
    {
        // Arrange
        var userId = 5;
        _holdingService.Setup(h => h.UserExistsAsync(userId)).ReturnsAsync(false).Verifiable();

        // Act
        var result = await _holdingController.GetHoldings(userId);

        // Assert
        _holdingService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(404, statusCodeResult.StatusCode);
        var returnObject = Assert.IsAssignableFrom<string>(statusCodeResult.Value);
        Assert.Equal("UserId 5 was not found", returnObject);
    }

    [Fact]
    public async Task GetHoldings_ProblemResult()
    {
        // Arrange
        var userId = 5;
        _holdingService.Setup(h => h.UserExistsAsync(userId)).ReturnsAsync(true).Verifiable();
        _holdingService.Setup(h => h.GetHoldingsAsync(userId)).Throws(new Exception("Bad")).Verifiable();

        // Act
        var result = await _holdingController.GetHoldings(userId);

        // Assert
        _holdingService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var problemDetails = Assert.IsAssignableFrom<ProblemDetails>(statusCodeResult.Value);
        Assert.True(_spyLogger.LoggerWasCalled);
        Assert.Equal(LogLevel.Error, _spyLogger.Logs[0].LogLevel);
        Assert.Equal("Error getting holdings data", _spyLogger.Logs[0].Message);
        Assert.NotNull(_spyLogger.Logs[0].Ex);
        Assert.Equal("Bad", _spyLogger.Logs[0].Ex?.Message);
        Assert.NotNull(problemDetails);
        Assert.Equal("Error getting holdings data", problemDetails.Detail);
    }

    [Fact]
    public async Task GetHoldingDetails_OkResult()
    {
        // Arrange
        var userId = 5;
        _holdingService.Setup(h => h.UserExistsAsync(userId)).ReturnsAsync(true).Verifiable();
        _holdingService.Setup(h => h.GetHoldingDetailsAsync(userId, It.IsAny<string>())).ReturnsAsync(holdingSample).Verifiable();

        // Act
        var result = await _holdingController.GetHoldingDetails(userId, "IBM");

        // Assert
        _holdingService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(200, statusCodeResult.StatusCode);
        var returnObject = Assert.IsAssignableFrom<InvestmentDto>(statusCodeResult.Value);
        Assert.NotNull(returnObject);
        Assert.Equal(holdingSample.CostBasisPerShare, returnObject.CostBasisPerShare);
        Assert.Equal(holdingSample.CurrentValue, returnObject.CurrentValue);
        Assert.Equal(holdingSample.CurrentPrice, returnObject.CurrentPrice);
        Assert.Equal(holdingSample.Term, returnObject.Term);
        Assert.Equal(holdingSample.TotalGainOrLoss, returnObject.TotalGainOrLoss);
        Assert.False(_spyLogger.LoggerWasCalled);
    }


    [Fact]
    public async Task GetHoldingDetails_UserNotFoundResult()
    {
        // Arrange
        var userId = 5;
        _holdingService.Setup(h => h.UserExistsAsync(userId)).ReturnsAsync(false).Verifiable();

        // Act
        var result = await _holdingController.GetHoldingDetails(userId, "IBM");

        // Assert
        _holdingService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(404, statusCodeResult.StatusCode);
        var returnObject = Assert.IsAssignableFrom<string>(statusCodeResult.Value);
        Assert.Equal("UserId 5 was not found", returnObject);
    }

    [Fact]
    public async Task GetHoldingDetails_InvestmentNotFoundResult()
    {
        // Arrange
        var userId = 5;
        _holdingService.Setup(h => h.UserExistsAsync(userId)).ReturnsAsync(true).Verifiable();
        _holdingService.Setup(h => h.GetHoldingDetailsAsync(userId, It.IsAny<string>())).ReturnsAsync((InvestmentDto?)null).Verifiable();

        // Act
        var result = await _holdingController.GetHoldingDetails(userId, "IBM");

        // Assert
        _holdingService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(404, statusCodeResult.StatusCode);
        var returnObject = Assert.IsAssignableFrom<string>(statusCodeResult.Value);
        Assert.Equal("InvestmentId IBM was not found", returnObject);
    }


    [Fact]
    public async Task GetHoldingDetails_ProblemResult()
    {
        // Arrange
        var userId = 5;
        _holdingService.Setup(h => h.UserExistsAsync(userId)).ReturnsAsync(true).Verifiable();
        _holdingService.Setup(h => h.GetHoldingDetailsAsync(userId, It.IsAny<string>())).Throws(new Exception("Bad")).Verifiable();

        // Act
        var result = await _holdingController.GetHoldingDetails(userId, "IBM");

        // Assert
        _holdingService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var problemDetails = Assert.IsAssignableFrom<ProblemDetails>(statusCodeResult.Value);
        Assert.True(_spyLogger.LoggerWasCalled);
        Assert.Equal(LogLevel.Error, _spyLogger.Logs[0].LogLevel);
        Assert.Equal("Error getting holding item data", _spyLogger.Logs[0].Message);
        Assert.NotNull(_spyLogger.Logs[0].Ex);
        Assert.Equal("Bad", _spyLogger.Logs[0].Ex?.Message);
        Assert.NotNull(problemDetails);
        Assert.Equal("Error getting holding item data", problemDetails.Detail);
    }
}
