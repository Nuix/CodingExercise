using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business.Models
{
    public class UserInvestmentDetailsVM
    {
        public decimal AmountOfShares { get; set; }

        public decimal SharePurchasePrice { get; set; }

        public decimal CurrentValue { get; set; }

        public decimal CurrentPrice { get; set; }

        public string Term { get; set; }

        public decimal GainLoss { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int ListingId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int UserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DateTime PurchaseDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string CompanyName { get; set; }
    }
}
