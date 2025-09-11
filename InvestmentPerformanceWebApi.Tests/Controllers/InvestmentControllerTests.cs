using System.Threading;
using System.Threading.Tasks;
using InvestmentPerformanceWebApi.Controllers;
using InvestmentPerformanceWebApi.Models;
using InvestmentPerformanceWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InvestmentPerformanceWebApi.Tests.Controllers;

public class InvestmentControllerTests
{
    private readonly InvestmentController _controller;
    private readonly Mock<IInvestmentService> _serviceMock;

    public InvestmentControllerTests()
    {
        _serviceMock = new Mock<IInvestmentService>();
        _controller = new InvestmentController(Mock.Of<ILogger<InvestmentController>>(), _serviceMock.Object);
    }

    [Fact]
    public async Task Get_WithNonPositiveUserId_ReturnsBadRequest()
    {
        var result = await _controller.Get(0, CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("UserId must be a positive integer.", badRequest.Value);
        _serviceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Get_WithValidUserId_ReturnsOkWithInvestments()
    {
        var userId = 42;
        var expected = new List<InvestmentDto>
        {
            new(1, "ETF A"),
            new(2, "Stock B")
        };

        _serviceMock
            .Setup(s => s.GetInvestmentsByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _controller.Get(userId, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var actual = Assert.IsAssignableFrom<IReadOnlyList<InvestmentDto>>(ok.Value);
        Assert.Equal(expected.Count, actual.Count);
        _serviceMock.VerifyAll();
    }

    [Fact]
    public async Task Get_WhenCancelled_Returns499()
    {
        var userId = 7;

        _serviceMock
            .Setup(s => s.GetInvestmentsByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        var result = await _controller.Get(userId, CancellationToken.None);

        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(499, objectResult.StatusCode);
        Assert.Equal("Client Closed Request", objectResult.Value);
        _serviceMock.VerifyAll();
    }

    [Fact]
    public async Task Get_WhenUnhandledException_Returns500()
    {
        var userId = 9;

        _serviceMock
            .Setup(s => s.GetInvestmentsByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("boom"));

        var result = await _controller.Get(userId, CancellationToken.None);

        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("An error occurred while retrieving investments.", objectResult.Value);
        _serviceMock.VerifyAll();
    }

    [Fact]
    public async Task GetDetails_WithInvalidIds_ReturnsBadRequest()
    {
        var result = await _controller.GetDetails(0, -1, CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("UserId and InvestmentId must be positive integers.", badRequest.Value);
        _serviceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetDetails_NotFound_ReturnsNotFound()
    {
        var userId = 5;
        var investmentId = 123;

        _serviceMock
            .Setup(s => s.GetInvestmentDetailsAsync(userId, investmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((InvestmentDetailDto?)null);

        var result = await _controller.GetDetails(userId, investmentId, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result.Result);
        _serviceMock.VerifyAll();
    }

    [Fact]
    public async Task GetDetails_Found_ReturnsOkWithDetails()
    {
        var userId = 5;
        var investmentId = 123;

        var expected =
            new InvestmentDetailDto(investmentId, userId, "Bond C", 1000m, 100m, 1500m, 1000m, "2022-01-01", 10m);
   
        _serviceMock
            .Setup(s => s.GetInvestmentDetailsAsync(userId, investmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _controller.GetDetails(userId, investmentId, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var actual = Assert.IsType<InvestmentDetailDto>(ok.Value);
        Assert.Equal(expected.Id, actual.Id);
        _serviceMock.VerifyAll();
    }

    [Fact]
    public async Task GetDetails_WhenCancelled_Returns499()
    {
        var userId = 2;
        var investmentId = 3;

        _serviceMock
            .Setup(s => s.GetInvestmentDetailsAsync(userId, investmentId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        var result = await _controller.GetDetails(userId, investmentId, CancellationToken.None);

        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(499, objectResult.StatusCode);
        Assert.Equal("Client Closed Request", objectResult.Value);
        _serviceMock.VerifyAll();
    }

    [Fact]
    public async Task GetDetails_WhenUnhandledException_Returns500()
    {
        var userId = 2;
        var investmentId = 3;

        _serviceMock
            .Setup(s => s.GetInvestmentDetailsAsync(userId, investmentId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("unexpected"));

        var result = await _controller.GetDetails(userId, investmentId, CancellationToken.None);

        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("An error occurred while retrieving investment details.", objectResult.Value);
        _serviceMock.VerifyAll();
    }

    [Fact]
    public void GetHealth_ReturnsOk()
    {
        var result = _controller.Get();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Investment API is running.", ok.Value);
    }
}
