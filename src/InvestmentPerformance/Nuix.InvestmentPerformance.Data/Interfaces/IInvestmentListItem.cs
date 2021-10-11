using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Interfaces
{
    interface IInvestmentListItem
    {
        int InvestmentId { get; set; }
        string Name { get; set; }
    }
}
