namespace InvestmentWebApi.Models;

public class InvestmentSummary {
  public InvestmentSummary(int investmentId, string name) {
    InvestmentId = investmentId;
    Name = name;
  }

  /// <summary>
  /// Unique ID for this investment.
  /// </summary>
  public int InvestmentId { get; }
  
  /// <summary>
  /// Name of the stock held in this investment.
  /// </summary>
  public string Name { get; }

  private bool Equals(InvestmentSummary other) {
    return InvestmentId == other.InvestmentId && Name == other.Name;
  }

  public override bool Equals(object? obj) {
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != GetType()) return false;
    return Equals((InvestmentSummary)obj);
  }

  public override int GetHashCode() {
    return HashCode.Combine(InvestmentId, Name);
  }
}