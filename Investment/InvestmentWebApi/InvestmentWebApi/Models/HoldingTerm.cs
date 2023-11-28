namespace InvestmentWebApi.Models;

/// <summary>
/// Specifies whether a given holding is long term (>1 year) or short term (<=1 year).
/// </summary>
public enum HoldingTerm {
  Short,
  Long
}