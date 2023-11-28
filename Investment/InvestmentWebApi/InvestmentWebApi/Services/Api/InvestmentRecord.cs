namespace InvestmentWebApi.Services.Api;

public class InvestmentRecord {
  /// <summary>
  /// Unique ID for this investment.
  /// </summary>
  public int InvestmentId { get; init; }
  
  /// <summary>
  /// ID of the user holding this investment.
  /// </summary>
  public int UserId { get; init; }
  
  /// <summary>
  /// ID of the stock held in this investment
  /// </summary>
  public int StockId { get; init; }
  
  /// <summary>
  /// Name of the stock held.
  /// </summary>
  public string? StockName { get; init; }
  
  /// <summary>
  /// Number of shares of the stock held in this investment.
  /// </summary>
  public decimal ShareCount { get; init; }
  
  /// <summary>
  /// Date the shares were acquired.
  /// </summary>
  public DateTime AcquireDate { get; init; }
}