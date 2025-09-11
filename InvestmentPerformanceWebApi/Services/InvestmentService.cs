using InvestmentPerformanceWebApi.Models;
using InvestmentPerformanceWebApi.Repositories;

namespace InvestmentPerformanceWebApi.Services;

public class InvestmentService(IInvestmentRepository repository, ILogger<InvestmentService> logger) : IInvestmentService
{
    private readonly IInvestmentRepository _repository = repository;
    private readonly ILogger<InvestmentService> _logger = logger;

    public async Task<IReadOnlyList<InvestmentDto>> GetInvestmentsByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching investments for user {UserId}", userId);
        var investments = await _repository.GetByUserAsync(userId, cancellationToken);

        return investments
            .OrderBy(i => i.Name)
            .Select(i => new InvestmentDto(i.Id, i.Name))
            .ToList();
    }

    public async Task<InvestmentDetailDto?> GetInvestmentDetailsAsync(int userId, int investmentId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching investment {InvestmentId} for user {UserId}", investmentId, userId);
        var i = await _repository.GetByUserAndIdAsync(userId, investmentId, cancellationToken);
        if (i is null) return null;

        return new InvestmentDetailDto(
            i.Id,
            i.UserId,
            i.Name,
            i.NumberOfShares,
            i.CostBasisPerShare,
            i.CurrentPrice,
            i.CurrentValue,
            i.Term,
            i.TotalGainLoss
        );
    }
}