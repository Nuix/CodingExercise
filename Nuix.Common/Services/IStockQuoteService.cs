using Nuix.Common.Models;
using System.Threading.Tasks;

namespace Nuix.Common.Services
{
  public interface IStockQuoteService
  {
    Task<StockQuote> GetQuote(string symbol);
  }
}