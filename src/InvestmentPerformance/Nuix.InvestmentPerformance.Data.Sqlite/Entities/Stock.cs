using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Sqlite.Entities
{
    public class Stock:IStock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }

    }
}
