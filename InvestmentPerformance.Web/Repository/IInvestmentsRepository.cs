using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPerformance.Web.Models.Entities;

namespace InvestmentPerformance.Web.Repository
{
    public interface IInvestmentsRepository
    {
        Task<List<UserInvestments>> SelectInvestmentsByUserIdAsync(int userId);
        Task<UserInvestments> SelectInvestmentsByUserIdAndInvestmentIdAsync(int userId, int investmentId);
    }
}