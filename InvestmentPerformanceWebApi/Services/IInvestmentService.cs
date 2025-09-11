using InvestmentPerformanceWebApi.Models;

namespace InvestmentPerformanceWebApi.Services;

public interface IInvestmentService
{
    Task<IReadOnlyList<InvestmentDto>> GetInvestmentsByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<InvestmentDetailDto?> GetInvestmentDetailsAsync(int userId, int investmentId, CancellationToken cancellationToken);
}
