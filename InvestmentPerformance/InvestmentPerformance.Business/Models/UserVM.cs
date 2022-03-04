using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business.Models
{
    public class UserVM
    {
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string LastName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string FirstName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
