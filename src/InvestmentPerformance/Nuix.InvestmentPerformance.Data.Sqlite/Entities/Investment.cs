using Nuix.InvestmentPerformance.Data.Enums;
using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Sqlite.Entities
{
    public class Investment : IInvestment
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }
        public string Symbol { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CostBasis { get; set; }
        public int NumberOfShares { get; set; }
    }
}
