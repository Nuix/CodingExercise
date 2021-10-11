using Nuix.InvestmentPerformance.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Interfaces
{
    public interface IUserConnector
    {
       IUser GetUser(int id);
    }
}
