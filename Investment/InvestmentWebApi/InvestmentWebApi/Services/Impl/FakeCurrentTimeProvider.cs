using InvestmentWebApi.Services.Api;

namespace InvestmentWebApi.Services.Impl; 

public class FakeCurrentTimeProvider : ICurrentTimeProvider {
  private DateTime? _now;

  public void SetNow(DateTime now) {
    _now = now;
  }
  
  public DateTime GetNow() {
    if (_now == null) {
      return DateTime.Now;
    }
    return _now.Value;
  }
}