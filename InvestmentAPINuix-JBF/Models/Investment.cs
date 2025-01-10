using System;
using System.Collections.Generic;

namespace InvestmentAPINuix_JBF.Models;

public partial class Investment
{
    public int Id { get; set; }

    public string Company { get; set; } = null!;

    public decimal NumberOfShares { get; set; }

    public decimal CostBasisPerShare { get; set; }

    public decimal CurrentPrice { get; set; }

    public DateOnly DatePurchased { get; set; }

    public string Term { get; set; } = null!;

    public decimal StartingValue { get; set; }

    public decimal CurrentValue { get; set; }

    public decimal TotalValue { get; set; }

    public int Investor { get; set; }

    public virtual User InvestorNavigation { get; set; } = null!;
}
