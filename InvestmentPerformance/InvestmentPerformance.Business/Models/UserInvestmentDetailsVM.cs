using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business.Models
{
    public class UserInvestmentDetailsVM
    {
        public int Id { get; set; }

        public int ListingId { get; set; }

        public int UserId { get; set; }

        public decimal AmountOfShares { get; set; }

        public decimal SharePurchasePrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Term
        {
            get
            {
                if (DateTime.UtcNow < PurchaseDate.AddYears(1))
                {
                    return "Short";
                }

                return "Long";
            }
        }

        public decimal PurchaseValue
        {
            get
            {
                return SharePurchasePrice * AmountOfShares;
            }
        }

        public decimal CurrentValue { get; set; }

        public decimal GainLoss { get; set; }        

        public string CompanyName { get; set; }
    }
}
