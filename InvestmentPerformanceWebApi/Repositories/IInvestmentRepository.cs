using InvestmentPerformanceWebApi.Models;

namespace InvestmentPerformanceWebApi.Repositories;

public interface IInvestmentRepository
{
    Task<IReadOnlyList<Investment>> GetByUserAsync(int userId, CancellationToken cancellationToken);
    Task<Investment?> GetByUserAndIdAsync(int userId, int investmentId, CancellationToken cancellationToken);
}
