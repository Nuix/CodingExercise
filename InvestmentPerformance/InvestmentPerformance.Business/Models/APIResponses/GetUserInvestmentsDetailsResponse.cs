using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business.Models.APIResponses
{
    public  class GetUserInvestmentsDetailsResponse
    {
        public List<UserInvestmentDetailsVM> UserInvestments { get; set; }
    }
}
