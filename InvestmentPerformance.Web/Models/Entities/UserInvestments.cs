using System;
using InvestmentPerformance.Web.Enums;

namespace InvestmentPerformance.Web.Models.Entities
{
    public class UserInvestments : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int InvestmentId { get; set; }
        public Investment Investment { get; set; }
        
        public decimal CostBasis { get; set; }
        public decimal Quantity { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Gain { get; set; }

        public InvestmentStatus InvestmentStatus { get; set; } = InvestmentStatus.Active;
        public InvestmentTerm InvestmentTerm { get; set; } = InvestmentTerm.ShortTerm;
        public DateTimeOffset AcquireDateUtc { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset SellDateUtc { get; set; }
    }
}