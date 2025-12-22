using InvestmentPerformanceWebApi.Domain.Models;

namespace InvestmentPerformanceWebApi.Services.Interfaces
{
    public interface IInvestmentService
    {
        Task<IEnumerable<Investment>> GetInvestmentsForUserAsync(int userId);
        Task<Investment> GetInvestmentForUserAsync(int userId, int investmentId);
    }
}
