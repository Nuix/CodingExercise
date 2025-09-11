using InvestmentPerformanceWebApi.Data;
using InvestmentPerformanceWebApi.Models;

namespace InvestmentPerformanceWebApi.Repositories;

public class InMemoryInvestmentRepository(InvestmentContext context, ILogger<InMemoryInvestmentRepository> logger) : IInvestmentRepository
{
    private readonly InvestmentContext _context = context;
    private readonly ILogger<InMemoryInvestmentRepository> _logger = logger;

    public Task<IReadOnlyList<Investment>> GetByUserAsync(int userId, CancellationToken cancellationToken)
    {
        // AsQueryable to allow future extensions (filtering/sorting)
        var list = _context.Investments
            .Where(i => i.UserId == userId)
            .ToList()
            .AsReadOnly();

        return Task.FromResult<IReadOnlyList<Investment>>(list);
    }

    public Task<Investment?> GetByUserAndIdAsync(int userId, int investmentId, CancellationToken cancellationToken)
    {
        var investment = _context.Investments.FirstOrDefault(i => i.UserId == userId && i.Id == investmentId);
        return Task.FromResult(investment);
    }
}