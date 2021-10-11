using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Dtos
{
    public class UserDto : IUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
