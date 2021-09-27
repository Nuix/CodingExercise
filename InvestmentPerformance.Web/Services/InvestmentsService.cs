using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentPerformance.Web.Enums;
using InvestmentPerformance.Web.Models.DTOs;
using InvestmentPerformance.Web.Models.Extensions;
using InvestmentPerformance.Web.Repository;
using Microsoft.Extensions.Logging;

namespace InvestmentPerformance.Web.Services
{
    public class InvestmentsService : IInvestmentsService
    {
        private readonly IInvestmentsRepository _investmentsRepository;
        private readonly IInvestmentPriceService _investmentPriceService;
        private readonly ILogger<InvestmentsService> _logger;
        
        private readonly int INVESTMENT_TERM_THREASHHOLD_IN_DAYS = 365;

        public InvestmentsService(
            IInvestmentsRepository investmentsRepository,
            IInvestmentPriceService investmentPriceService,
            ILogger<InvestmentsService> logger)
        {
            _investmentsRepository = investmentsRepository;
            _investmentPriceService = investmentPriceService;
            _logger = logger;
        }
        
        public async Task<List<InvestmentDto>> GetInvestmentsByUserIdAsync(int userId)
        {
            _logger.LogTrace("Getting investments for {userId}", userId);

            var response = await _investmentsRepository
                .SelectInvestmentsByUserIdAsync(userId);

            if (null == response)
            {
                var message = $"Received null response while getting investments for {userId}";
                throw new Exception(message);
            }
            
            _logger.LogTrace("Received {rawInvestments} records from storage for user {userId}",
                 response?.Count ?? 0, userId);
            
            var dtoResponse = response.Select(item => item.AsInvestmentDto()).ToList();
            
            _logger.LogTrace("Transformed user investments to {userInvestmentsDto} for {userId}",
                dtoResponse.ToString(), userId);

            return dtoResponse;
        }
        
        public async Task<InvestmentDetailsDto> GetInvestmentsByUserIdAndInvestmentIdAsync(int userId, 
            int investmentId)
        {
            _logger.LogTrace("Getting investment details for {userId} and {investmentId}", userId, investmentId);
            
            // Get investment details from storage
            var userInvestment = await _investmentsRepository
                .SelectInvestmentsByUserIdAndInvestmentIdAsync(userId, investmentId);

            if (null == userInvestment)
            {
                var message = $"Received null response while getting investment details for user {userId} and investment {investmentId}";
                throw new Exception(message);
            }

            // If Investment is Inactive just return it
            if (userInvestment.InvestmentStatus == InvestmentStatus.Inactive)
                return userInvestment.AsInvestmentDetailDto(); 
            
            // The Investment is still active so perform live calculations
            var originalValue = userInvestment.Quantity * userInvestment.CostBasis;
            
            // Get current investment price from the pricing service
            var currentPrice = (decimal) await _investmentPriceService.GetPriceAsync(
                userInvestment.Investment.Name, userInvestment.Investment.InvestmentType);
            
            var currentValue = userInvestment.Quantity * currentPrice;
            
            userInvestment.CurrentPrice = currentPrice;
            userInvestment.CurrentValue = currentValue;
            userInvestment.Gain = currentValue - originalValue;
            
            userInvestment.InvestmentTerm = 
                (userInvestment.SellDateUtc - userInvestment.AcquireDateUtc).TotalDays 
                                                > INVESTMENT_TERM_THREASHHOLD_IN_DAYS 
                    ? InvestmentTerm.LongTerm 
                    : InvestmentTerm.ShortTerm;
            
            var dtoResponse = userInvestment.AsInvestmentDetailDto();
            
            _logger.LogTrace("Transformed user investment details to {dtoResponse} for {userId} and {investmentId}",
                dtoResponse.ToString(), userId, investmentId);

            return dtoResponse;
        }
    }
}