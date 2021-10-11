using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Sqlite.Entities
{
    public class Price :IPrice
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
