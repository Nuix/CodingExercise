using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestmentPerformance.Data.Model;

namespace InvestmentPerformance.Business.Models
{
    internal static class Extensions
    {
        public static ListingVM MapFrom(this ListingVM mapTo, Listing mapFrom)
        {
            return new ListingVM
            {
                CompanyName = mapFrom.CompanyName,
                CurrentPrice = mapFrom.CurrentPrice,
                Id = mapFrom.Id
            };
        }

        public static UserInvestmentDetailsVM MapFrom(this UserInvestmentDetailsVM mapTo, UserInvestment mapFrom)
        {
            return new UserInvestmentDetailsVM
            {
                Id = mapFrom.Id,
                AmountOfShares = mapFrom.AmountOfShares,
                ListingId = mapFrom.ListingId,
                PurchaseDate = mapFrom.PurchaseDate,
                SharePurchasePrice = mapFrom.SharePurchasePrice,    
                UserId = mapFrom.UserId,                
                CompanyName = mapFrom.Listing.CompanyName
            };
        }
    }
}
