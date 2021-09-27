using InvestmentPerformance.Web.Models.DTOs;
using InvestmentPerformance.Web.Models.Entities;

namespace InvestmentPerformance.Web.Models.Extensions
{
    public static class Extensions
    {
        public static InvestmentDto AsInvestmentDto(this UserInvestments investment)
        { 
            return new InvestmentDto(investment.Id, investment.Investment.Name);
        }

        public static InvestmentDetailsDto AsInvestmentDetailDto(this UserInvestments investments)
        {
            return new InvestmentDetailsDto(investments.Quantity, investments.CostBasis,
                investments.CurrentPrice, investments.CurrentValue,
                investments.InvestmentTerm, investments.Gain);
        }
    }
}