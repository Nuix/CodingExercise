using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Interfaces
{
    public interface IUser
    {
        int UserId { get; set; }
        string Name { get; set; }
    }
}
