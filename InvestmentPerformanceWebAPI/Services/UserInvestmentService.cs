using InvestmentPerformanceWebAPI.Database.Contexts;
using InvestmentPerformanceWebAPI.DTOs;
using InvestmentPerformanceWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceWebAPI.Services
{
    /// <summary>
    /// Service that encapsulates the business logic for retrieving user investments and computing performance metrics.
    /// </summary>
    public class UserInvestmentService(InvestmentDbContext dbContext) : IUserInvestmentService
    {
        private readonly InvestmentDbContext _dbContext = dbContext;

        /// <summary>
        /// Returns a summary list of all investments for the specified user.
        /// </summary>
        public async Task<UserInvestmentsDTO> GetUserInvestmentsAsync(int userId)
        {
            var investments = await _dbContext.Investments
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return new UserInvestmentsDTO
            {
                Investments = investments.Select(i => new UserInvestmentSummaryDTO
                {
                    InvestmentId = i.InvestmentId,
                    Name = i.Name
                }).ToList()
            };
        }

        /// <summary>
        /// Returns detailed information about a specific investment owned by the user.
        /// Computes derived values such as current value, term, and total gain/loss.
        /// </summary>
        public async Task<InvestmentDTO?> GetInvestmentAsync(int userId, int investmentId)
        {
            var investment = await _dbContext.Investments
                .FirstOrDefaultAsync(i => i.UserId == userId && i.InvestmentId == investmentId);

            if (investment is null)
            {
                return null;
            }

            var currentValue = investment.NumberOfShares * investment.CurrentPrice;
            var amountPaid = investment.NumberOfShares * investment.CostBasisPerShare;
            var gainLoss = currentValue - amountPaid;

            var term = CalculateTerm(investment.TimeOfPurchase, DateTime.UtcNow);

            return new InvestmentDTO
            {
                InvestmentId = investment.InvestmentId,
                Name = investment.Name,
                Ticker = investment.Ticker,
                NumberOfShares = investment.NumberOfShares,
                CostBasisPerShare = investment.CostBasisPerShare,
                CurrentPrice = investment.CurrentPrice,
                CurrentValue = currentValue,
                Term = term,
                TotalGainLoss = gainLoss
            };
        }

        /// <summary>
        /// Calculates the investment term classification ("ShortTerm" or "LongTerm")
        /// based on the difference between the purchase date and the current date.
        /// </summary>
        private static string CalculateTerm(DateTime purchaseDate, DateTime nowUtc)
        {
            var daysHeld = (nowUtc.Date - purchaseDate.Date).TotalDays;
            return daysHeld <= 365 ? "ShortTerm" : "LongTerm";
        }
    }
}
