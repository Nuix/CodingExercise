using Nuix.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace Nuix.Common.Services
{
  public class StockQuoteService : IStockQuoteService
  {
    public async Task<StockQuote> GetQuote(string symbol)
    {
      IReadOnlyDictionary<string, Security> securities = await Yahoo.Symbols(symbol).Fields(Field.RegularMarketPrice).QueryAsync();
      if (securities.ContainsKey(symbol))
      {
        return new StockQuote
        {
          QuotedUTC = System.DateTime.UtcNow,
          Quote = (decimal)securities[symbol].RegularMarketPrice
        };
      }

      return null;
    }
  }
}