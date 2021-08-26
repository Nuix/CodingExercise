using System;

namespace Nuix.Common.Models
{
  public class InvestmentDetails : InvestmentDetailsLight
  {
    public DateTime PurchasedUtc { get; set; }
    public double Quantity { get; set; }
    public decimal CostBasis { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal CurrentPrice { get; set; }
    public bool IsLongTerm { get; set; }
    public decimal GainLoss { get; set; }

    public void ApplyQuote(StockQuote currentQuote)
    {
      CurrentValue = currentQuote.Quote * (decimal)Quantity;
      CurrentPrice = currentQuote.Quote;
      IsLongTerm = (currentQuote.QuotedUTC - DateTime.SpecifyKind(PurchasedUtc, DateTimeKind.Utc)).TotalDays >= 365;
      GainLoss = CurrentValue - (CostBasis * (decimal)Quantity);
    }
  }
}