namespace InvestmentWebApi.Models; 

public class InvestmentDetails {
  public enum HoldingTerm {
    Short,
    Long
  }
  
  public int InvestmentId { get; set; }
  public String Name { get; set; }
  
  public Decimal ShareCount { get; set; }
  public Decimal CostBasis { get; set; }
  public Decimal CurrentValue { get; set; }
  public Decimal CurrentPrice { get; set; }
  public HoldingTerm Term { get; set; }
  public Decimal TotalGainLoss { get; set; }
}