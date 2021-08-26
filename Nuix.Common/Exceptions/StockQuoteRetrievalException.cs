using System;

namespace Nuix.Common.Exceptions
{
  public class StockQuoteRetrievalException : Exception
  {
    public StockQuoteRetrievalException(string message) : base(message) {}
  }
}