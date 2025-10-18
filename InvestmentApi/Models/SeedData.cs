using System.Collections.Generic;

namespace InvestmentApi.Models
{
    public class SeedData
    {
        public List<User> Users { get; set; } = new();
        public List<Investment> Investments { get; set; } = new();
    }
}
