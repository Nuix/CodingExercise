using InvestmentWebApi.Data.Api;
using InvestmentWebApi.Models;
using InvestmentWebApi.Services.Api;

namespace InvestmentWebApi.Data.Impl;

public class PortfolioManager : IPortfolioManager {
  private readonly IDbReader _dbReader;
  private readonly ICurrentTimeProvider _currentTimeProvider;


  public PortfolioManager(IDbReader dbReader, ICurrentTimeProvider currentTimeProvider) {
    _dbReader = dbReader;
    _currentTimeProvider = currentTimeProvider;
  }

  public ICollection<InvestmentSummary> GetInvestments(int userId) {
    IEnumerable<InvestmentRecord> investmentRecords = _dbReader.GetInvestmentsForUser(userId);

    return investmentRecords.Select(i =>
        new InvestmentSummary(i.InvestmentId, i.StockName)
    ).ToList();
  }

  public InvestmentDetails? GetInvestmentDetails(int investmentId) {
    var record = _dbReader.GetInvestmentDetails(investmentId);

    if (record == null) {
      return null;
    }

    var holdingTerm = GetHoldingTerm(record);

    return new InvestmentDetails(
        shareCount: record.ShareCount,
        costBasis: record.CostBasis,
        currentValue: GetCurrentValue(record),
        currentPrice: record.CurrentPrice,
        term: holdingTerm,
        totalGainLoss: GetTotalGainLoss(record),
        termString: holdingTerm.ToString("g"));
  }

  private decimal GetCurrentValue(InvestmentDetailedRecord record) {
    return record.CurrentPrice * record.ShareCount;
  }

  private HoldingTerm GetHoldingTerm(InvestmentDetailedRecord record) {
    if (record.AcquireDate.AddYears(1) < _currentTimeProvider.GetNow()) {
      return HoldingTerm.Long;
    }
    return HoldingTerm.Short;
  }

  private decimal GetTotalGainLoss(InvestmentDetailedRecord record) {
    return GetCurrentValue(record) - 
           record.CostBasis * record.ShareCount;
  }
}