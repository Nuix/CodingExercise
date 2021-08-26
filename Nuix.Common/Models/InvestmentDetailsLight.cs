using System;

namespace Nuix.Common.Models
{
  public class InvestmentDetailsLight : IEquatable<InvestmentDetailsLight>
  {
    public long InvestmentId { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }

    public override bool Equals(object obj)
    {
      return Equals(obj as InvestmentDetailsLight);
    }

    public bool Equals(InvestmentDetailsLight other)
    {
      return other != null
        && InvestmentId == other.InvestmentId
        && Name == other.Name
        && Symbol == other.Symbol;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(InvestmentId, Name, Symbol);
    }
  }
}