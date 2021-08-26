using System;

namespace Nuix.Api.Data.Entities
{
  public partial class Investment
  {
    public long InvestmentId { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public DateTime PurchasedUtc { get; set; }
    public double Quantity { get; set; }
    public decimal CostBasis { get; set; }
    public virtual User User { get; set; }
  }
}