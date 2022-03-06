using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestmentPerformance.Data.Model;

namespace InvestmentPerformance.Business.Models
{
    public static class Mappers
    {
        public static UserVM MapFrom(this UserVM mapTo, User mapFrom)
        {
            return new UserVM
            {
                Id = mapFrom.Id,
                FullName = $"{mapFrom.FirstName} {mapFrom.LastName}"
            };
        }

        public static UserInvestmentVM MapFrom(this UserInvestmentVM mapTo, UserInvestment mapFrom)
        {
            return new UserInvestmentVM
            {
                InvestmentId = mapFrom.Id,
                CompanyName = mapFrom.Listing.CompanyName
            };
        }

        public static UserInvestmentDetailsVM MapFrom(this UserInvestmentDetailsVM mapTo, UserInvestment mapFrom)
        {
            var currentValue = mapFrom.Listing.CurrentPrice * mapFrom.AmountOfShares;
            var gainLoss = currentValue - (mapFrom.SharePurchasePrice * mapFrom.AmountOfShares);
            var term = DateTime.UtcNow < mapFrom.PurchaseDate.AddYears(1) 
                ? Constants.Short 
                : Constants.Long;

            return new UserInvestmentDetailsVM
            {
                AmountOfShares = mapFrom.AmountOfShares,
                SharePurchasePrice = mapFrom.SharePurchasePrice,
                CurrentPrice = mapFrom.Listing.CurrentPrice,
                GainLoss = gainLoss,
                CurrentValue = currentValue,
                Term = term
            };
        }
    }
}
