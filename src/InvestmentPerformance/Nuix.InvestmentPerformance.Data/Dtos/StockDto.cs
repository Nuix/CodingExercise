using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nuix.InvestmentPerformance.Data.Interfaces;

namespace Nuix.InvestmentPerformance.Data.Dtos
{
    public class StockDto :IStock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        IEnumerable<PriceDto> Prices { get; set; }
    }
}
