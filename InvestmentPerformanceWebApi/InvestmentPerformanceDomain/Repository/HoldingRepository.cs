using InvestmentPerformanceDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceDomain.Repository;

public interface IHoldingRepository
{
    Task<IList<Holding>> GetHoldingsAsync(int userId);
    Task<Holding?> GetHoldingAsync(int userId, string symbol);
}

public class HoldingRepository : IHoldingRepository
{
    private readonly InvestmentPerformanceContext _context;

    public HoldingRepository(InvestmentPerformanceContext context)
    {
        _context = context;
    }

    public async Task<Holding?> GetHoldingAsync(int userId, string symbol)
    {
        return await _context.Holdings
            .Where(h => h.UserId == userId && h.Symbol.ToUpper() == symbol.ToUpper())
            .FirstOrDefaultAsync();
    }

    public async Task<IList<Holding>> GetHoldingsAsync(int userId)
    {
        return await _context.Holdings
            .Where(h => h.UserId == userId)
            .OrderBy(h => h.Symbol)
            .ToListAsync();
    }
}

