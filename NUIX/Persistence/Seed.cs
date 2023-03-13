using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData (DataBaseContext context)
        {
            //Seeding Investment type table
            if(context.StockInvestments.Any())
                return;

            var investingTypes = new List<InvestmentType> {

                new InvestmentType{
                    InvestmentTypeId = 1,
                    InvestmentName="Stock"
                },
                new InvestmentType{
                    InvestmentTypeId = 2,
                    InvestmentName="Bond"
                },
                new InvestmentType{
                    InvestmentTypeId = 3,
                    InvestmentName="Mutual Fund"
                }
            };

            await context.InvestmentTypes.AddRangeAsync(investingTypes);


            var investments = new List<Investment>
            {
                new Investment
                {
                    InvestmentId= 1,
                    InvestmentTypeId = 1,
                    UserId= 1,
                    InvestmentName = "User 1 stocks Investment"
                },
                new Investment
                {
                    InvestmentId= 2,
                    InvestmentTypeId = 1,
                    UserId= 2,
                    InvestmentName = "User 2 stocks Investment"
                },

                new Investment
                {
                    InvestmentId= 3,
                    InvestmentTypeId = 1,
                    UserId= 3,
                    InvestmentName = "User 3 stocks Investment"
                }
            };
            await context.Investments.AddRangeAsync(investments);

            var stocks = new List<Stock>
            {
                new Stock
                {
                    StockId= 1,
                    StockName = "Amazon",
                    SharePrice = 80.44m
                },
                new Stock
                {
                    StockId= 2,
                    StockName = "Google",
                    SharePrice = 91.05m 
                },
                new Stock
                {
                    StockId= 3,
                    StockName = "Microsoft",
                    SharePrice = 92.25m
                }
            };
            await context.Stocks.AddRangeAsync(stocks);

            var stockInvestments = new List<StockInvestment>
            {
                new StockInvestment {
                    InvestmentId= 1,
                    StockId= 1,
                    PurchasedDate = DateTime.Now.AddMonths(-13),
                    PricePerShare = 90.73m,
                    SharesQuantity= 150
                },
                new StockInvestment {
                    InvestmentId= 2,
                    StockId= 2,
                    PurchasedDate = DateTime.Now.AddMonths(-2),
                    PricePerShare = 75.10m,
                    SharesQuantity= 250
                },
                new StockInvestment {
                    InvestmentId= 3,
                    StockId= 3,
                    PurchasedDate = DateTime.Now.AddMonths(-1),
                    PricePerShare = 93.56m,
                    SharesQuantity= 50
                }
            };
            await context.StockInvestments.AddRangeAsync(stockInvestments);
            await context.SaveChangesAsync();
        }
    }
}
