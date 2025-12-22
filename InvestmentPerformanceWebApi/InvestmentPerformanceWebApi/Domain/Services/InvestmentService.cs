using InvestmentPerformanceWebApi.DbInfrastructure;
using InvestmentPerformanceWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceWebApi.Domain.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly AppDbContext _context;

        public InvestmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Investment>> GetInvestmentsForUserAsync(int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if(!userExists)
            {
                throw new KeyNotFoundException($"User with Id {userId} not found");
            }

            var investments = await _context.Investments
                .Where(i => i.UserId == userId)
                .Select(x => new Models.Investment(x.Name, x.Id, x.Shares, x.CostBasis, x.CurrentPrice, x.PurchasedAtUtc))
                .ToListAsync();

            return investments;


        }

        public async Task<Models.Investment> GetInvestmentForUserAsync(int userId, int investmentId)
        {

            var investment = await _context.Investments
                .Where(i => i.UserId == userId && i.Id == investmentId)
                .Select(x => new Models.Investment(x.Name, x.Id, x.Shares, x.CostBasis, x.CurrentPrice, x.PurchasedAtUtc))
                .FirstOrDefaultAsync();

            if (investment is null)
            {
                throw new KeyNotFoundException($"Either Investment {investmentId} or user {userId} invalid");
            }

            return investment;
        }
    }
}
