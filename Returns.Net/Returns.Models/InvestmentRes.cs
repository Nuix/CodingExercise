using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Returns.Models.Enum;

namespace Returns.Models;

[DebuggerDisplay("{ToString(),nq}")]
public sealed record InvestmentRes(
    Guid Id, //prefer Guids as easier for scaling + security
    Guid UserId,
    string Name,
    decimal ShareCount,
    decimal CostBasisPerShare,
    DateTime PurchaseDate,
    decimal CurrentPrice)
{
    //usually i would ignore this data on serialization unless its needed since they are all computed
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TermEnum Term => DateTime.UtcNow.Subtract(PurchaseDate).TotalDays > 365 ? TermEnum.Long : TermEnum.Short;
    
    public decimal TotalGainOrLoss => CurrentValue - (ShareCount * CostBasisPerShare);

    public decimal CurrentValue => ShareCount * CurrentPrice;
    
    public override string ToString() => $"{Name} ({ShareCount} shares at {CostBasisPerShare:C} per share purchased on {PurchaseDate:d})";
}

