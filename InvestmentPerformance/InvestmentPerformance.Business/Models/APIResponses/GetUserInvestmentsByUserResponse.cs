using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business.Models.APIResponses
{
    public class GetUserInvestmentsByUserResponse
    {
        public UserVM User { get; set; }

        public List<UserInvestmentVM> Investments { get; set; }
    }
}
