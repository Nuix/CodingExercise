using System.Collections.Generic;

namespace InvestmentPerformance.Web.Models.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<UserInvestments> Investments { get; set; }
    }
}