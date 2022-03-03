using InvestmentPerformance.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business
{
    public class BusinessService
    {
        public decimal GetCurrentValue(Listing listing, UserInvestment userInvestment)
        {
            return listing.CurrentPrice * userInvestment.AmountOfShares;
        }

        public decimal GetGainLoss(Listing listing, UserInvestment userInvestment)
        {
            return GetCurrentValue(listing, userInvestment) - userInvestment.PurchaseValue;
        }        
    }
}
