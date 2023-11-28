using InvestmentWebApi.Services.Api;

namespace InvestmentWebApi.Services.Impl; 

public class CurrentTimeProvider : ICurrentTimeProvider {
  public DateTime GetNow() {
    return DateTime.Now;
  }
}