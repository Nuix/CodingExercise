namespace InvestmentWebApi.Services.Api;

public class InvestmentDetailedRecord : InvestmentRecord {
  
  /// <summary>
  /// The price of a single share at the time this position was opened.
  /// </summary>
  public decimal CostBasis { get; set; }

  
  /// <summary>
  /// The current price of a single share of the stock.
  /// </summary>
  public decimal CurrentPrice { get; set; }
}