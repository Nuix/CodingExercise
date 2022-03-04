using InvestmentPerformance.Business.Models;
using InvestmentPerformance.Business.Models.APIResponses;
using InvestmentPerformance.Data;
using InvestmentPerformance.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business
{
    public class BusinessService : IBusinessService
    {
        public async Task<GetUserInvestmentsByUserResponse> GetUserInvestmentVMs(int userId)
        {
            var response = new GetUserInvestmentsByUserResponse
            {
                Investments = new List<UserInvestmentDetailsVM>()
            };

            using (var context = new InvestmentPerformanceContext())
            {
                var user = context.Users.Find(userId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var userInvestments = await context.UserInvestments
                    .Include(ui => ui.Listing)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

                response.User = new UserVM
                {
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName
                };

                foreach (var ui in userInvestments)
                {
                    var newVM = new UserInvestmentDetailsVM().MapFrom(ui);
                    newVM.CurrentValue = GetCurrentValue(ui.Listing, newVM);
                    newVM.GainLoss = GetGainLoss(ui.Listing, newVM);
                    response.Investments.Add(newVM);
                }

            }

            return response;
        }

        public async Task<GetUserInvestmentsDetailsResponse> GetUserInvestmentsDetails(int investmentId)
        {
            var response = new GetUserInvestmentsDetailsResponse
            {
                UserInvestments = new List<UserInvestmentDetailsVM>()
            };

            using (var context = new InvestmentPerformanceContext())
            {
                var userInvestments = await context.UserInvestments
                .Include(ui => ui.Listing)
                .Where(x => x.Id == investmentId)
                .ToListAsync();

                foreach (var ui in userInvestments)
                {
                    var newVM = new UserInvestmentDetailsVM().MapFrom(ui);
                    newVM.CurrentValue = GetCurrentValue(ui.Listing, newVM);
                    newVM.GainLoss = GetGainLoss(ui.Listing, newVM);
                    response.UserInvestments.Add(newVM);
                }
            }

            return response;
        }
        
        public decimal GetCurrentValue(Listing listing, UserInvestmentDetailsVM userInvestment)
        {
            return listing.CurrentPrice * userInvestment.AmountOfShares;
        }

        public decimal GetGainLoss(Listing listing, UserInvestmentDetailsVM userInvestment)
        {
            return GetCurrentValue(listing, userInvestment) - userInvestment.PurchaseValue;
        }
    }
}
