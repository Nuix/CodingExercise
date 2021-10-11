using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Sqlite.Connectors
{
    public class PriceConnector : IPriceConnector
    {
        private readonly ILogger _logger;
        private readonly InvestmentPerformanceContext _db;

        public PriceConnector(ILogger<PriceConnector> logger,IServiceProvider serviceProvider)
        {
            _logger = logger;
            _db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<InvestmentPerformanceContext>();

        }


        public decimal GetCurrentPrice(string symbol)
        {

            decimal price = 0;
            try
            {
                price = _db.Prices.Where(p => p.Symbol == symbol).OrderByDescending(p => p.Date).FirstOrDefault()?.Value ?? 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return price;
        }
    }
}
