using System.Collections.Generic;
using InvestmentPerformance.Web.Enums;

namespace InvestmentPerformance.Web.Models.Entities
{
    public class Investment : BaseEntity
    {
        public string Name { get; set; }
        public InvestmentType InvestmentType { get; set; } = InvestmentType.Stock;
        public IEnumerable<UserInvestments> Users { get; set; }
    }
}