using System;
using System.Collections.Generic;

namespace InvestmentAPINuix_JBF.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Investment> Investments { get; set; } = new List<Investment>();
}
