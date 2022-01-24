using InvestmentPerformanceDomain.Models;
using InvestmentPerformanceDomain.Repository;
using InvestmentPerformanceWebApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPerformanceWebApi.Test.Services;

public class HoldingServiceTest
{
    private readonly HoldingService _holdingService;
    private readonly Mock<IPricingService> _pricingService = new Mock<IPricingService>();
    private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
    private readonly Mock<IHoldingRepository> _holdingRepository = new Mock<IHoldingRepository>();


    private readonly List<Holding> holdingsSample = new List<Holding>
    {
        new Holding
        {
            UserId= 5,
            Symbol= "IBM",
            Name= "INTERNATIONAL BUSINESS MACHINES CORPORATION",
            HoldingType=InvestmentType.Stock,
            Cost = 100m,
            Amount=10m,
            OpenDate= DateTime.Now.AddYears(-2),
        },
        new Holding
        {
            UserId= 5,
            Symbol= "TSLA",
            Name= "TESLA, INC.",
            HoldingType=InvestmentType.Stock,
            Cost = 55.12m,
            Amount= 3m,
            OpenDate= DateTime.Now.AddMonths(-1),
        }
    };

    private readonly Holding holdingBondSample = new Holding
    {
        UserId = 5,
        Symbol = "64971Q7G2",
        Name = "New York New York City Transitional Taxable Subordinated Future Tax Secured",
        HoldingType = InvestmentType.Bond,
        Cost = 101.75m,
        Amount = 1000m,
        OpenDate = DateTime.Now.AddMonths(-6),
    };


    public HoldingServiceTest()
    {
        _holdingService = new HoldingService(_pricingService.Object, _userRepository.Object, _holdingRepository.Object);
    }

    [Fact]
    public async Task UserExistsAsync_WithTrueResults()
    {
        // Arrange
        _userRepository.Setup(u => u.UserExistsAsync(It.IsAny<int>())).ReturnsAsync(true).Verifiable();

        // Act
        var result = await _holdingService.UserExistsAsync(5);

        // Assert
        _userRepository.Verify();
        Assert.True(result);
    }

    [Fact]
    public async Task UserExistsAsync_WithFalseResults()
    {
        // Arrange
        _userRepository.Setup(u => u.UserExistsAsync(It.IsAny<int>())).ReturnsAsync(false).Verifiable();

        // Act
        var result = await _holdingService.UserExistsAsync(5);

        // Assert
        _userRepository.Verify();
        Assert.False(result);
    }


    [Fact]
    public async Task UserExistsAsync_WithException()
    {
        // Arrange
        _userRepository.Setup(h => h.UserExistsAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Bad")).Verifiable();
        Exception? exception = null;

        // Act
        try
        {
            var result = await _holdingService.UserExistsAsync(5);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        _userRepository.Verify();
        Assert.NotNull(exception);
        Assert.Equal("Bad", exception?.Message);
    }

    [Fact]
    public async void GetHoldingsAsync_WithResults()
    {
        // Arrange
        _holdingRepository.Setup(h => h.GetHoldingsAsync(It.IsAny<int>())).ReturnsAsync(holdingsSample).Verifiable();

        // Act
        var result = await _holdingService.GetHoldingsAsync(5);

        // Assert
        _holdingRepository.Verify();
        Assert.NotNull(result);
        Assert.Equal(holdingsSample.Count, result.Count);
        Assert.Equal(holdingsSample[0].Symbol, result[0].Id);
        Assert.Equal(holdingsSample[0].Name, result[0].Name);
        Assert.Equal(holdingsSample[1].Symbol, result[1].Id);
        Assert.Equal(holdingsSample[1].Name, result[1].Name);
    }

    [Fact]
    public async void GetHoldingsAsync_WithEmptyResults()
    {
        // Arrange
        _holdingRepository.Setup(h => h.GetHoldingsAsync(It.IsAny<int>())).ReturnsAsync(new List<Holding>()).Verifiable();

        // Act
        var result = await _holdingService.GetHoldingsAsync(5);

        // Assert
        _holdingRepository.Verify();
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async void GetHoldingsAsync_WithException()
    {
        // Arrange
        _holdingRepository.Setup(h => h.GetHoldingsAsync(It.IsAny<int>())).Throws(new Exception("Bad")).Verifiable();
        Exception? exception = null;

        // Act
        try
        {
            var result = await _holdingService.GetHoldingsAsync(5);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        _holdingRepository.Verify();
        Assert.NotNull(exception);
        Assert.Equal("Bad", exception?.Message);
    }

    [Fact]
    public async Task GetHoldingDetailsAsync_WithData()
    {
        // Arrange
        var price = 111.11m;
        _pricingService
            .Setup(p => p.GetPriceAsync(It.IsAny<string>()))
            .ReturnsAsync(price)
            .Verifiable();
        _holdingRepository
            .Setup(h => h.GetHoldingAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(holdingsSample[0])
            .Verifiable();

        // Act
        var result = await _holdingService.GetHoldingDetailsAsync(5, "IBM");

        // Assert
        _pricingService.Verify();
        _holdingRepository.Verify();
        Assert.NotNull(result);
        Assert.Equal(holdingsSample[0].Cost, result?.CostBasisPerShare);
        Assert.Equal(1111.10m, result?.CurrentValue);
        Assert.Equal(price, result?.CurrentPrice);
        Assert.Equal("Long Term", result?.Term);
        Assert.Equal(111.10m, result?.TotalGainOrLoss);
    }


    [Fact]
    public async Task GetHoldingDetailsAsync_WithNullPrice()
    {
        // Arrange
        _pricingService
            .Setup(p => p.GetPriceAsync(It.IsAny<string>()))
            .ReturnsAsync((decimal?)null)
            .Verifiable();
        _holdingRepository
            .Setup(h => h.GetHoldingAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(holdingsSample[0])
            .Verifiable();

        // Act
        var result = await _holdingService.GetHoldingDetailsAsync(5, "IBM");

        // Assert
        _pricingService.Verify();
        _holdingRepository.Verify();
        Assert.NotNull(result);
        Assert.Equal(holdingsSample[0].Cost, result?.CostBasisPerShare);
        Assert.Null(result?.CurrentValue);
        Assert.Null(result?.CurrentPrice);
        Assert.Equal("Long Term", result?.Term);
        Assert.Null(result?.TotalGainOrLoss);
    }

    public async Task GetHoldingDetailsAsync_WithNullHolding()
    {

        // Arrange
        _holdingRepository
            .Setup(h => h.GetHoldingAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync((Holding?)null)
            .Verifiable();

        // Act
        var result = await _holdingService.GetHoldingDetailsAsync(5, "IBM");

        // Assert
        _holdingRepository.Verify();
        Assert.Null(result);
    }


    [Fact]
    public async Task GetHoldingDetailsAsync_WithShortTermData()
    {
        // Arrange
        var price = 55.55m;
        _holdingRepository
            .Setup(h => h.GetHoldingAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(holdingsSample[1])
            .Verifiable();
        _pricingService
            .Setup(p => p.GetPriceAsync(It.IsAny<string>()))
            .ReturnsAsync(price)
            .Verifiable();

        // Act
        var result = await _holdingService.GetHoldingDetailsAsync(5, "TSLA");

        // Assert
        _holdingRepository.Verify();
        _pricingService.Verify();
        Assert.NotNull(result);
        Assert.Equal(holdingsSample[1].Cost, result?.CostBasisPerShare);
        Assert.Equal(166.65m, result?.CurrentValue);
        Assert.Equal(price, result?.CurrentPrice);
        Assert.Equal("Short Term", result?.Term);
        Assert.Equal(1.29m, result?.TotalGainOrLoss);
    }

    [Fact]
    public async Task GetHoldingDetailsAsync_WithNegativeGainsData()
    {
        // Arrange
        var price = 5.55m;
        _holdingRepository
            .Setup(h => h.GetHoldingAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(holdingsSample[1])
            .Verifiable();
        _pricingService
            .Setup(p => p.GetPriceAsync(It.IsAny<string>()))
            .ReturnsAsync(price)
            .Verifiable();

        // Act
        var result = await _holdingService.GetHoldingDetailsAsync(5, "TSLA");

        // Assert
        _holdingRepository.Verify();
        _pricingService.Verify();
        Assert.NotNull(result);
        Assert.Equal(holdingsSample[1].Cost, result?.CostBasisPerShare);
        Assert.Equal(16.65m, result?.CurrentValue);
        Assert.Equal(price, result?.CurrentPrice);
        Assert.Equal("Short Term", result?.Term);
        Assert.Equal(-148.71m, result?.TotalGainOrLoss);
    }



    [Fact]
    public async Task GetHoldingDetailsAsync_WithBondData()
    {
        // Arrange
        _holdingRepository
            .Setup(h => h.GetHoldingAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(holdingBondSample)
            .Verifiable();

        // Act
        var result = await _holdingService.GetHoldingDetailsAsync(5, "64971Q7G2");

        // Assert
        _holdingRepository.Verify();
        Assert.NotNull(result);
        Assert.Equal(holdingBondSample.Cost, result?.CostBasisPerShare);
        Assert.Equal(100000m, result?.CurrentValue);
        Assert.Equal(100m, result?.CurrentPrice);
        Assert.Equal("Short Term", result?.Term);
        Assert.Equal(-1750m, result?.TotalGainOrLoss);
    }

    [Fact]
    public async void GetHoldingDetailsAsync_WithHoldingException()
    {
        // Arrange
        _holdingRepository
            .Setup(h => h.GetHoldingAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Bad"))
            .Verifiable();
        Exception? exception = null;


        // Act
        try
        {
            var result = await _holdingService.GetHoldingDetailsAsync(5, "TSLA");
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        _holdingRepository.Verify();
        Assert.NotNull(exception);
        Assert.Equal("Bad", exception?.Message);
    }


    [Fact]
    public async void GetHoldingDetailsAsync_WithPricingException()
    {
        // Arrange
        _holdingRepository
            .Setup(h => h.GetHoldingAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(holdingsSample[1])
            .Verifiable();
        _pricingService
            .Setup(p => p.GetPriceAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Bad"))
            .Verifiable();
        Exception? exception = null;


        // Act
        try
        {
            var result = await _holdingService.GetHoldingDetailsAsync(5, "TSLA");
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        _holdingRepository.Verify();
        _pricingService.Verify();
        Assert.NotNull(exception);
        Assert.Equal("Bad", exception?.Message);
    }
}
