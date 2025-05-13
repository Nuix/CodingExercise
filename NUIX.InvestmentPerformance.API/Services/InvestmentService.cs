using NUIX.InvestmentPerformance.API.Data;
using NUIX.InvestmentPerformance.API.DataTransferObjects;
using NUIX.InvestmentPerformance.API.Models;
using NUIX.InvestmentPerformance.API.Models.Enums;
using InvestmentDbContext = NUIX.InvestmentPerformance.API.Data.InvestmentDbContext;

namespace NUIX.InvestmentPerformance.API.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly InvestmentDbContext _context;

        public InvestmentService()
        {

        }

        public InvestmentService(InvestmentDbContext context)
        {
            _context = context;
        }

        public List<InvestmentSummaryDTO> GetInvestmentsForUser(Guid userId)
        {
            return _context.Investments
                .Where(i => i.UserID == userId)
                .Select(i => new InvestmentSummaryDTO
                {
                    InvestmentID = i.InvestmentID,
                    InvestmentName = i.InvestmentName
                })
                .ToList();
        }

        public InvestmentDetailDTO? GetInvestmentDetails(Guid userId, Guid investmentId)
        {
            var investment = _context.Investments
                .FirstOrDefault(i => i.InvestmentID == investmentId && i.UserID == userId);

            if (investment == null)
                return null;

            return CalculateInvestmentDetails(investment);
        }

        public InvestmentDetailDTO CalculateInvestmentDetails(Investment investment)
        {

            var currentValue = investment.NumberOfShares * investment.CurrentPricePerShare;
            var term = (DateTime.UtcNow - investment.PurchaseDate).TotalDays > 365 ? InvestmentTerm.LongTerm : InvestmentTerm.ShortTerm;
            var totalCost = investment.NumberOfShares * investment.CostBasisPerShare;
            var gainLoss = currentValue - totalCost;

            return new InvestmentDetailDTO
            {
                InvestmentID = investment.InvestmentID,
                InvestmentName = investment.InvestmentName,
                NumberOfShares = investment.NumberOfShares,
                CostBasisPerShare = investment.CostBasisPerShare,
                CurrentPrice = investment.CurrentPricePerShare,
                CurrentValue = currentValue,
                Term = term,
                TotalGainOrLoss = gainLoss
            };
        }
    }

}
