using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentPerformance.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Web.Repository
{
    public class InvestmentsRepository : IInvestmentsRepository
    {
        private readonly InvestmentPerformanceDbContext _db;
        
        public InvestmentsRepository(InvestmentPerformanceDbContext investmentPerformanceDbContext)
        {
            _db = investmentPerformanceDbContext;
        }

        public async Task<List<UserInvestments>> SelectInvestmentsByUserIdAsync(int userId)
        {
            return await _db.UserInvestments
                .Include(x => x.Investment)
                .Where(x => x.User.Id == userId).ToListAsync();
        }

        public async Task<UserInvestments> SelectInvestmentsByUserIdAndInvestmentIdAsync(int userId, int investmentId)
        {
            return await _db.UserInvestments
                .Include(x => x.Investment)
                .FirstOrDefaultAsync(x => x.User.Id == userId && 
                                          x.Investment.Id == investmentId);
        }
    }
}