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
        public async Task<GetUserInvestmentsByUserResponse> GetUserInvestmentsByUser(int userId)
        {
            var response = new GetUserInvestmentsByUserResponse
            {
                Investments = new List<UserInvestmentVM>()
            };

            try
            {
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

                    response.User = new UserVM().MapFrom(user);
                    response.Investments = userInvestments.Select(x => new UserInvestmentVM().MapFrom(x)).ToList();
                }
            }
            catch (Exception)
            {
                // log error
                throw;
            }            

            return response;
        }

        public async Task<GetUserInvestmentsDetailsResponse> GetUserInvestmentsDetails(int investmentId)
        {
            var response = new GetUserInvestmentsDetailsResponse();

            try
            {
                using (var context = new InvestmentPerformanceContext())
                {
                    var userInvestments = await context.UserInvestments
                    .Include(ui => ui.Listing)
                    .Where(x => x.Id == investmentId)
                    .ToListAsync();

                    response.UserInvestments = userInvestments.Select(x => new UserInvestmentDetailsVM().MapFrom(x)).ToList();
                }
            }
            catch (Exception)
            {
                // log error
                throw;
            }            

            return response;
        }

    }
}
