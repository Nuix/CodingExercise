using InvestmentWebApi.Services.Api;

namespace InvestmentWebApi.Services.Impl; 

public class FakeDbReader : IDbReader {
  private Dictionary<int, List<InvestmentRecord>> _investments = new();

  private Dictionary<int, InvestmentDetailedRecord?> _investmentsDetailed = new();

  public void AddInvestment(InvestmentRecord investment) {
    if (!_investments.ContainsKey(investment.UserId)) {
      _investments[investment.UserId] = new List<InvestmentRecord>();
    }
    _investments[investment.UserId].Add(investment);
  }
  
  public void AddInvestmentDetailed(InvestmentDetailedRecord? investment) {
    _investmentsDetailed[investment.InvestmentId] = investment;
  }
  
  
  public ICollection<InvestmentRecord> GetInvestmentsForUser(int userId) {
    if (!_investments.ContainsKey(userId)) {
      return new List<InvestmentRecord>();
    }
    return _investments[userId];
  }

  public InvestmentDetailedRecord? GetInvestmentDetails(int investmentId) {
    if (!_investmentsDetailed.ContainsKey(investmentId)) {
      return null;
    }
    return _investmentsDetailed[investmentId];
  }
}