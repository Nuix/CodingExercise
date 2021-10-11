using Nuix.InvestmentPerformance.Data.Enums;
using Nuix.InvestmentPerformance.Data.Interfaces;
using System;

namespace Nuix.InvestmentPerformance.Data.Dtos

{
    public class InvestmentDto :IInvestment
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CostBasis { get; set; }
        public int NumberOfShares { get; set; }
        public string Symbol { get; set; }
        public IStock Stock { get; set; }
        public Terms Term { get; set; }
        public decimal Value { get; set; }
        public decimal Price { get; set; }

        public decimal TotalGain { get; set; }
    }
}
