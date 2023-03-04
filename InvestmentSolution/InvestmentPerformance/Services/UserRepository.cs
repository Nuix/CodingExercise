using System;
using InvestmentPerformance.Models;
using InvestmentPerformance.Database;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Services
{
    public class UserRepository : IUserRepository
    {

        static UserRepository() 
        {
            using (var context = new ApiContext())
            {
                Guid UId1 = new Guid("f78c3d80-857d-407e-b7dd-8d2e9416fc78");
                Guid UId2 = System.Guid.NewGuid();

                Guid IId1 = new Guid("480a8608-6611-46a7-a3a4-16d7b5cdfe9f");
                Guid IId2 = System.Guid.NewGuid();

                Guid OId1 = System.Guid.NewGuid();
                Guid OId2 = System.Guid.NewGuid();


                var users = new List<User>
                {
                    new User
                    {
                        Id = UId1,
                        Name = "Test User1",
                    },
                    new User
                    {
                        Id = UId2,
                        Name = "Test User2",
                    }
                };

                var investments = new List<Investment>
                {
                    new Investment
                    {
                        Id = IId1,
                        Name = "MSFT",
                    },
                    new Investment
                    {
                        Id = IId2,
                        Name = "TSLA",
                    }
                };

                var investmentDetails = new List<InvestmentDetails>
                {
                    new InvestmentDetails
                    {
                        OwnershipId = OId1,
                        UserId = UId1,
                        InvestmentId = IId1,
                        PurchaseDateTime = new DateTime(2023,01,02,4,3,0),
                        CostBasis = 1000,
                        CurrentPrice = 1100,
                        NumberOfShares = 1
                    },
                    new InvestmentDetails
                    {
                        OwnershipId = OId2,
                        UserId = UId1,
                        InvestmentId = IId2,
                        PurchaseDateTime = new DateTime(2020,05,09,9,15,0),
                        CostBasis = 500,
                        CurrentPrice = 550,
                        NumberOfShares = 10
                    }
                };

                context.Users.AddRange(users);
                context.Investments.AddRange(investments);
                context.InvestmentDetails.AddRange(investmentDetails);
                context.SaveChanges();
            }

        }

        public async Task<List<User>> GetUsersAsync()
        {
            using (var context = new ApiContext())
            {
                try
                {
                    var list = context.Users
                                .ToListAsync();
                    return await list;
                }

                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public async Task<List<InvestmentDetails>> GetInvestmentDetailsAsync()
        {
            using (var context = new ApiContext())
            {
                try
                {
                    var list = context.InvestmentDetails
                                //.Include(a => a.InvestmentDetails)
                                .ToListAsync();
                    return await list;
                }

                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public async Task<List<Investment>> GetInvestmentByIdAsync(Guid id)
        {
            using (var context = new ApiContext())
            {
                try
                {
                    var list = context.InvestmentDetails
                                .Where(a => a.UserId == id)
                                .Join(context.Investments,
                                id => id.InvestmentId,
                                iid => iid.Id,
                                (id, iid) => new Investment { Id = iid.Id, Name = iid.Name} )
                                .ToListAsync();
                    return await list;
                }

                catch (Exception ex)
                {
                    return null;
                }

            }
        }
        
        public async Task<List<InvestmentDetails>> GetInvestmentDetailsAsync(Guid userid, Guid investmentId)
        {
            using (var context = new ApiContext())
            {
                try
                {
                    var list = context.InvestmentDetails
                                .Where(a => a.UserId == userid && a.InvestmentId == investmentId)
                                .ToListAsync();
                    return await list;
                }

                catch (Exception ex)
                {
                    return null;
                }

            }
        }

    }
}

