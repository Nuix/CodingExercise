using InvestmentWebApi.Models;

namespace InvestmentWebApi.Services.Api;

public interface IDbReader {
  /// Returns all investment records for the specified user.
  ICollection<InvestmentRecord> GetInvestmentsForUser(int userId);

  /// Returns the details of a specific holding.
  InvestmentDetailedRecord? GetInvestmentDetails(int investmentId);
}