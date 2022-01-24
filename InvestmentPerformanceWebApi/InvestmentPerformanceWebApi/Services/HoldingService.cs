using InvestmentPerformanceDomain.Models;
using InvestmentPerformanceDomain.Repository;
using InvestmentPerformanceWebApi.DTOs;

namespace InvestmentPerformanceWebApi.Services;

public interface IHoldingService
{
    Task<bool> UserExistsAsync(int userId);
    Task<InvestmentDto?> GetHoldingDetailsAsync(int userId, string id);
    Task<IList<InvestmentSummaryDto>> GetHoldingsAsync(int userId);
}

public class HoldingService : IHoldingService
{
    private readonly IUserRepository _userRepository;
    private readonly IPricingService _pricingService;
    private readonly IHoldingRepository _holdingRepository;

    public HoldingService(IPricingService pricingService,
        IUserRepository userRepository,
        IHoldingRepository holdingRepository)
    {
        _userRepository = userRepository;
        _pricingService = pricingService;
        _holdingRepository = holdingRepository;
    }

    public async Task<InvestmentDto?> GetHoldingDetailsAsync(int userId, string id)
    {
        var holding = await _holdingRepository.GetHoldingAsync(userId, id);
        if (holding == null)
            return null;

        decimal? price = null;
        if (holding.HoldingType == InvestmentType.Stock || holding.HoldingType == InvestmentType.MutualFunds)
            price = await _pricingService.GetPriceAsync(id);
        else if (holding.HoldingType == InvestmentType.Bond)
            price = 100;

        return new InvestmentDto
        {
            // Cost basis per share: this is the price of 1 share of stock at the time it was purchased
            CostBasisPerShare = holding.Cost,
            // Current value: this is the number of shares multiplied by the current price per share
            CurrentValue = holding.Amount * price,
            // Current price: this is the current price of 1 share of the stock
            CurrentPrice = price,
            // Term: this is how long the stock has been owned. <= 1 year is short term, > 1 year is long term
            Term = holding.OpenDate < DateTime.Now.AddYears(-1) ? InvestmentDto.LONG_TERM : InvestmentDto.SHORT_TERM,
            // Total gain or loss: this is the difference between the current value, and the amount paid for all shares when they were purchased
            TotalGainOrLoss = (holding.Amount * price) - (holding.Amount * holding.Cost)
        };
    }

    public async Task<IList<InvestmentSummaryDto>> GetHoldingsAsync(int userId)
    {
        var holdings = await _holdingRepository.GetHoldingsAsync(userId);
        return holdings.Select(h => new InvestmentSummaryDto { Id = h.Symbol, Name = h.Name }).ToList();
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
        return await _userRepository.UserExistsAsync(userId);
    }
}

