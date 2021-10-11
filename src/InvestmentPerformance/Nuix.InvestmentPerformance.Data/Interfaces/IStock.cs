using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Interfaces
{
    public interface IStock
    {
        string Symbol { get; set; }
        string Name { get; set; }

    }
}
