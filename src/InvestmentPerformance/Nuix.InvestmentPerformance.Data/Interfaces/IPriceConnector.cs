using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Interfaces
{
   public interface IPriceConnector
    {
        decimal GetCurrentPrice(string symbol);
    }
}
