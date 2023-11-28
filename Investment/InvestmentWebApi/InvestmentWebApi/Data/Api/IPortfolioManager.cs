using InvestmentWebApi.Models;

namespace InvestmentWebApi.Data.Api;

/// <summary>
/// Retrieves the requested investment data.
/// </summary>
public interface IPortfolioManager {
  
  /// <summary>
  /// Get the investments for the given user.
  /// </summary>
  ICollection<InvestmentSummary> GetInvestments(int userId);
  
  /// <summary>
  /// Get the details of the investment for the given user.
  /// </summary>
  InvestmentDetails? GetInvestmentDetails(int investmentId);
}