using InvestmentPerformance.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly InvestmentContext _context;

        public InvestmentRepository(InvestmentContext context)
        {
            _context = context;
        }

        public async Task<Investment?> GetInvestmentById(int investmentId)
        {
            return await _context.Investments
                .Include(i => i.Stock)
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.ID == investmentId);
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByUserId(int userId)
        {
            return await _context.Investments
                .Where(i => i.UserID == userId)
                .Include(i => i.Stock)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
