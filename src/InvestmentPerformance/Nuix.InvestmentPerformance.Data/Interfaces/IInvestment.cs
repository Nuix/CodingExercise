using Nuix.InvestmentPerformance.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Interfaces
{
   public interface IInvestment
    {
        int InvestmentId { get; set; }
        int UserId { get; set; }
        string Symbol { get; set; }

        DateTime PurchaseDate { get; set; }
        decimal CostBasis { get; set; }
        int NumberOfShares { get; set; }

    }
}
