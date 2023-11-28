namespace InvestmentWebApi.Models;

public class InvestmentDetails {
  public InvestmentDetails(
      decimal shareCount, decimal costBasis,
      decimal currentValue, decimal currentPrice, HoldingTerm term,
      decimal totalGainLoss, string termString) {
    ShareCount = shareCount;
    CostBasis = costBasis;
    CurrentValue = currentValue;
    CurrentPrice = currentPrice;
    Term = term;
    TotalGainLoss = totalGainLoss;
    TermString = termString;
  }

  /// <summary>
  /// Number of shares the user is holding.
  /// </summary>
  public decimal ShareCount { get; }
  
  /// <summary>
  /// The price of a single share at the time this position was opened.
  /// </summary>
  public decimal CostBasis { get; }
  
  /// <summary>
  /// The current value of all of the shares of this stock held by the user.
  /// </summary>
  public decimal CurrentValue { get; }
  
  /// <summary>
  /// The current price of a single share of the stock.
  /// </summary>
  public decimal CurrentPrice { get; }
  
  /// <summary>
  /// Whether the shares are held long term or short term.
  /// </summary>
  public HoldingTerm Term { get; }
  
  /// <summary>
  /// Human readable version of the Term.
  /// </summary>
  public string TermString { get; }
  
  /// <summary>
  /// The difference the current value of all of the shares of the stock vs their value at acquisition.
  /// </summary>
  public decimal TotalGainLoss { get; }
}