using InvestmentPerformance.Web.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InvestmentPerformance.Web.Models.DTOs
{
    public record InvestmentDto(int Id, string Name);

    public record InvestmentDetailsDto(
        // Number of stock
        decimal Quantity,
        // Cost basis per share: this is the price of 1 share of stock at the time it was purchased
        decimal CostBasis,
        // Current price: this is the current price of 1 share of the stock
        decimal CurrentPrice,
        // Current value: this is the number of shares multiplied by the current price per share
        decimal CurrentValue,
        // Term: this is how long the stock has been owned. <=1 year is short term, >1 year is long term
        [JsonConverter(typeof(StringEnumConverter))]
        InvestmentTerm InvestmentTerm,
        // Total gain or loss: this is the difference between the current value, and the amount paid for all shares when they were purchased
        decimal Gain
    );
}