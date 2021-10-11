using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Dtos
{
    public class InvestListItemDto:IInvestmentListItem
    {
        public int InvestmentId { get; set; }
        public string Name { get; set; }
    }
}
