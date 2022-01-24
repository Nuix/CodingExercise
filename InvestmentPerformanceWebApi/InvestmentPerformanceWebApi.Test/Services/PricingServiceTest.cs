using InvestmentPerformanceWebApi.Configurations;
using InvestmentPerformanceWebApi.Services;
using InvestmentPerformanceWebApi.Test.Spys;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPerformanceWebApi.Test.Services;

public class PricingServiceTest
{
    private readonly PricingService _pricingService;

    private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
    private readonly Mock<IOptionsMonitor<PricingConfiguration>> _config = new Mock<IOptionsMonitor<PricingConfiguration>>();

    private readonly SpyHttpHandler _spyHttpHandler = new SpyHttpHandler();
    private readonly SpyLogger<PricingService> _spyLogger = new SpyLogger<PricingService>();

    private const string jsonResponse =
        "{" +
        "\"Global Quote\": " +
        "{" +
        "\"01. symbol\": \"IBM\"," +
        "\"02. open\": \"131.6500\"," +
        "\"03. high\": \"131.8700\"," +
        "\"04. low\": \"129.2700\"," +
        "\"05. price\": \"129.3500\"" +
        "}" +
        "}";

    public PricingServiceTest()
    {
        _config.Setup(c => c.CurrentValue).Returns(new PricingConfiguration { ApiKey = "XXXXX" });
        _pricingService = new PricingService(_config.Object, _httpClientFactory.Object, _spyLogger);
    }

    [Fact]
    public async Task GetPricing_Results()
    {
        // Arrange
        var symbol = "IBM";

        using (var pricingClient = new HttpClient(_spyHttpHandler))
        {
            pricingClient.BaseAddress = new Uri("https://www.alphavantage.co");

            _httpClientFactory
                .Setup(hcf => hcf.CreateClient(PricingService.PRICING_HTTPCLIENTNAME))
                .Returns(pricingClient);

            _spyHttpHandler.Setup(
                $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey=XXXXX",
                jsonResponse);

            // Act
            var result = await _pricingService.GetPriceAsync(symbol);

            // Assert
            Assert.Equal(129.35m, result);
            Assert.False(_spyLogger.LoggerWasCalled);
        }
    }

    [Fact]
    public async Task GetPricing_EmptyJsonResponse()
    {
        // Arrange
        var symbol = "IBM";

        using (var pricingClient = new HttpClient(_spyHttpHandler))
        {
            pricingClient.BaseAddress = new Uri("https://www.alphavantage.co");

            _httpClientFactory
                .Setup(hcf => hcf.CreateClient(PricingService.PRICING_HTTPCLIENTNAME))
                .Returns(pricingClient);

            _spyHttpHandler.Setup(
                $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey=XXXXX",
                "{}");

            // Act
            var result = await _pricingService.GetPriceAsync(symbol);

            // Assert
            Assert.Null(result);
            Assert.False(_spyLogger.LoggerWasCalled);
        }
    }

    [Fact]
    public async Task GetPricing_Non200Response()
    {
        // Arrange
        var symbol = "IBM";

        using (var pricingClient = new HttpClient(_spyHttpHandler))
        {
            pricingClient.BaseAddress = new Uri("https://www.alphavantage.co");

            _httpClientFactory
                .Setup(hcf => hcf.CreateClient(PricingService.PRICING_HTTPCLIENTNAME))
                .Returns(pricingClient);

            _spyHttpHandler.Setup(
                $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey=XXXXX",
                jsonResponse,
                HttpStatusCode.Forbidden);

            // Act
            var result = await _pricingService.GetPriceAsync(symbol);

            // Assert
            Assert.Null(result);
            Assert.True(_spyLogger.LoggerWasCalled);
            Assert.NotEmpty(_spyLogger.Logs);
            Assert.Equal("Error getting price for IBM", _spyLogger.Logs[0].Message);
            Assert.NotNull(_spyLogger.Logs[0].Ex);
        }
    }

    [Fact]
    public async Task GetPricing_InvalidResponse()
    {
        // Arrange
        var symbol = "IBM";

        using (var pricingClient = new HttpClient(_spyHttpHandler))
        {
            pricingClient.BaseAddress = new Uri("https://www.alphavantage.co");

            _httpClientFactory
                .Setup(hcf => hcf.CreateClient(PricingService.PRICING_HTTPCLIENTNAME))
                .Returns(pricingClient);

            _spyHttpHandler.Setup(
                $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey=XXXXX",
                "This is not JSON",
                HttpStatusCode.Forbidden);

            // Act
            var result = await _pricingService.GetPriceAsync(symbol);

            // Assert
            Assert.Null(result);
            Assert.True(_spyLogger.LoggerWasCalled);
            Assert.NotEmpty(_spyLogger.Logs);
            Assert.Equal("Error getting price for IBM", _spyLogger.Logs[0].Message);
            Assert.NotNull(_spyLogger.Logs[0].Ex);
        }
    }
}
