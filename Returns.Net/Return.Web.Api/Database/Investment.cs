using System;
using System.Diagnostics;

namespace Return.Web.Api.Database;

//Represents what would be in your dbcontext
[DebuggerDisplay("{ToString(),nq}")]
public class Investment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ShareName { get; set; }
    public decimal ShareCount { get; set; }
    public decimal CostBasisPerShare { get; set; }
    public DateTime PurchaseDateTime { get; set; }

    public Investment(){ }

    public Investment(Guid id, Guid userId, string shareName, decimal shareCount, decimal costBasisPerShare, DateTime purchaseDateTime)
    {
        Id = id;
        UserId = userId;
        ShareName = shareName;
        ShareCount = shareCount;
        CostBasisPerShare = costBasisPerShare;
        PurchaseDateTime = purchaseDateTime;
    }
    
    public override string ToString() => $"{ShareName} ({ShareCount} shares at {CostBasisPerShare:C} per share purchased on {PurchaseDateTime:d})";
}