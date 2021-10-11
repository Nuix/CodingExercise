using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Sqlite.Connectors
{
    public class StockConnector : IStockConnector
    {
        private readonly ILogger _logger;
        private readonly InvestmentPerformanceContext _db;

        public StockConnector(ILogger<StockConnector> logger,IServiceProvider serviceProvider)
        {
            _logger = logger;
            _db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<InvestmentPerformanceContext>();

        }
        public IStock GetStock(string symbol)
        {
            IStock stock = null;
            try
            {
                stock = _db.Stocks.FirstOrDefault(s => s.Symbol == symbol);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return stock;
        }
    }
}
