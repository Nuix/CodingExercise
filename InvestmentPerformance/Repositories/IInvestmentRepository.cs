using InvestmentPerformance.Models;

namespace InvestmentPerformance.Repositories
{
    public interface IInvestmentRepository
    {
        Task<IEnumerable<Investment>> GetInvestmentsByUserId(int userId);

        Task<Investment?> GetInvestmentById(int investmentId);
    }
}
