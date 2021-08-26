using System;

namespace Nuix.Common.Models
{
  public class StockQuote
  {
    public DateTime QuotedUTC { get; set; }
    public decimal Quote { get; set; }
  }
}