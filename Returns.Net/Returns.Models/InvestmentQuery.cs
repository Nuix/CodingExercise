using System;
using System.Diagnostics;

namespace Returns.Models;

[DebuggerDisplay("{ToString(),nq}")]
public class InvestmentQuery
{
    public Guid? UserId { get; set; }

    public int? Start { get; set; } = 0;

    public int? Amount { get; set; } = 10;

    public override string ToString() => $"InvestmentQuery: UserId={UserId}, Start={Start}, Amount={Amount}";
}