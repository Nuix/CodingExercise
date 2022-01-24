using InvestmentPerformanceWebApi.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;

namespace InvestmentPerformanceWebApi.Services;

public interface IPricingService
{
    Task<decimal?> GetPriceAsync(string symbol);
}

public class PricingService : IPricingService
{
    private readonly IOptionsMonitor<PricingConfiguration> _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PricingService> _logger;
    public const string PRICING_HTTPCLIENTNAME = "Pricing";
    public PricingService(IOptionsMonitor<PricingConfiguration> config, IHttpClientFactory httpClientFactory, ILogger<PricingService> logger)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<decimal?> GetPriceAsync(string symbol)
    {
        using (var pricingClient = _httpClientFactory.CreateClient(PRICING_HTTPCLIENTNAME))
        using (var response = await pricingClient.GetAsync($"query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_config.CurrentValue.ApiKey}"))
        {
            try
            {
                response.EnsureSuccessStatusCode();
                var stringResult = await response.Content.ReadAsStringAsync();
                var jNode = JsonNode.Parse(stringResult);
                if (jNode != null)
                {
                    var quote = jNode["Global Quote"];
                    if (quote != null)
                        return Convert.ToDecimal(quote["05. price"]?.GetValue<string>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting price for {symbol}", symbol);
            }
        }
        return null;

    }
}

