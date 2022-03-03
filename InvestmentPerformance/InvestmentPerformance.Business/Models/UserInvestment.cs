using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business.Models
{
    public class UserInvestment
    {
        public int Id { get; set; }

        public int ListingId { get; set; }

        public int UserId { get; set; }

        public decimal AmountOfShares { get; set; }

        public decimal SharePurchasePrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public TermEnum Term
        {
            get
            {
                if (DateTime.UtcNow < PurchaseDate.AddYears(1))
                {
                    return TermEnum.Short;
                }

                return TermEnum.Long;
            }
        }

        public decimal PurchaseValue
        {
            get
            {
                return SharePurchasePrice * AmountOfShares;
            }
        }
    }
}
