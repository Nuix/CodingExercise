using Domain.Entities;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class InvestmentRepository : Repository<Investment>, IInvestmentRepository
    {
        public InvestmentRepository(DataBaseContext context) : base(context)
        {
        }

        public int GetInvestmentTypeId(int investmentId)
        {
            var investment = _context.Investments.Find(investmentId);

            if(investment == null) 
                throw new Exception($"Investment data not found for id: {investmentId}");

            return investment.InvestmentTypeId;
        }
    }
}
