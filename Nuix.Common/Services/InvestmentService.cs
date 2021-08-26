using Microsoft.EntityFrameworkCore;
using Nuix.Api.Data.Contexts;
using Nuix.Common.Exceptions;
using Nuix.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nuix.Common.Services
{
  public class InvestmentService : IInvestmentService
  {
    private readonly INuixContext _context;
    private readonly Lazy<IStockQuoteService> _stockQuoteService;

    public InvestmentService(INuixContext context,
      Lazy<IStockQuoteService> stockQuoteService)
    {
      _context = context;
      _stockQuoteService = stockQuoteService;
    }

    public async Task<InvestmentDetails> GetUserInvestmentDetails(long userId, long investmentId)
    {
      //query for the investment
      InvestmentDetails investmentDetails =  await _context.Investments
        .Where(i => i.InvestmentId == investmentId && i.UserId == userId)
        .Select(i => new InvestmentDetails
        {
          InvestmentId = i.InvestmentId,
          Name = i.Name,
          Symbol = i.Symbol,
          PurchasedUtc = i.PurchasedUtc,
          Quantity = i.Quantity,
          CostBasis = i.CostBasis
        })
        .SingleOrDefaultAsync();

      if (investmentDetails != null
        && !string.IsNullOrWhiteSpace(investmentDetails.Symbol))
      {
        //lookup the current price and apply to the investment
        StockQuote currentQuote = await _stockQuoteService.Value.GetQuote(investmentDetails.Symbol.Trim());
        if (currentQuote != null)
        {
          investmentDetails.ApplyQuote(currentQuote);
          return investmentDetails;
        }
        throw(new StockQuoteRetrievalException($"Failed to retrieve a current stock quote for symbol \"{investmentDetails.Symbol.Trim()}\"."));
      }

      return null;
    }

    public async Task<IEnumerable<InvestmentDetailsLight>> GetUserInvestments(long userId)
    {
      //if you modify this make sure to profile it and verify you are not selecting too much data
      return await _context.Investments
        .Where(i => i.UserId == userId)
        .Select(i => new InvestmentDetailsLight
        {
          InvestmentId = i.InvestmentId,
          Name = i.Name,
          Symbol = i.Symbol
        })
        .ToListAsync();
    }
  }
}