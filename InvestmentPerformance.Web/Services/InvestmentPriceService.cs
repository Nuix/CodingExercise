using System;
using System.Threading.Tasks;
using InvestmentPerformance.Web.Enums;
using Microsoft.Extensions.Logging;

namespace InvestmentPerformance.Web.Services
{
    /// <summary>
    /// Fake Pricing Service - Returns a Random Investment price
    /// </summary>
    public class InvestmentPriceService : IInvestmentPriceService
    {
        private readonly ILogger<InvestmentPriceService> _logger;
        
        public InvestmentPriceService(ILogger<InvestmentPriceService> logger)
        {
            _logger = logger;
        }
        
        public Task<double> GetPriceAsync(string tickerSymbol, InvestmentType investmentType)
        {
            _logger.LogTrace("Getting Price for {tickerSymbol} and {investmentType}",
                tickerSymbol, investmentType);
            
            double price = 0.0;
            
            Random r = new Random();
            int rInt = r.Next(0, 100);
            
            switch (investmentType)
            {
                case InvestmentType.Stock:
                {
                    int range = 100;
                    price =  (double) (r.NextDouble() * range);
                    break;
                }
                case InvestmentType.Bond:
                {
                    int range = 10;
                    price =  (double) (r.NextDouble() * range);
                    break;
                }
                case InvestmentType.MutualFund:
                {
                    int range = 1;
                    price =  (double) (r.NextDouble() * range);
                    break;
                }
                default: break;
            }
            
            _logger.LogTrace("Returning {price} for {tickerSymbol} and {investmentType}",
                price, tickerSymbol, investmentType);
        
            return Task.FromResult(price);
        }
    }
}