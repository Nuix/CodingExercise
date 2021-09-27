using System.Collections.Generic;
using System.Threading.Tasks;
using InvestmentPerformance.Web.Models.DTOs;

namespace InvestmentPerformance.Web.Services
{
    public interface IInvestmentsService
    {
        Task<List<InvestmentDto>> GetInvestmentsByUserIdAsync(int userId);
        Task<InvestmentDetailsDto> GetInvestmentsByUserIdAndInvestmentIdAsync(int userId, int investmentId);
    }
}