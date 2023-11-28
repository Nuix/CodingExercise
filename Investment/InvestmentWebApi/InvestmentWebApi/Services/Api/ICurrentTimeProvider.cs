namespace InvestmentWebApi.Services.Api; 

/// <summary>
/// Wrapper around DateTime.Now to allow for dependency injection.
/// </summary>
public interface ICurrentTimeProvider {
  DateTime GetNow();
}