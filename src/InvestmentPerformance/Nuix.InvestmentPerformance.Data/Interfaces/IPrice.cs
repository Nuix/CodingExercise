using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Interfaces
{
    public interface IPrice
    {
         string Symbol { get; set; }
         DateTime Date { get; set; }
         decimal Value { get; set; }

    }
}
