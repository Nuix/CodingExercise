using Nuix_Project.APIObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nuix_Project.Data
{
    public class MockDataLayer : IDataLayer
    {
        public InvestmentDetails GetInvestmentDetailsByInvestmentId(long id)
        {
            var random = new Bogus.Randomizer();

            var randomDayCount = random.Number(1, 24);


            return new InvestmentDetails()
            {
                NumberOfShares = random.Int(0, 1000),
                CostBasisPerShare = random.Decimal(0.0m,10.0m),
                CurrentPrice = random.Decimal(0.0m, 10.0m),
                Id = random.Long(1, 100000),
                Name = random.Word(),
                PurchaseDateTime = DateTime.Now.AddMonths(-randomDayCount)
            };

        }

        public List<Investment> GetInvestmentsByUserId(long id)
        {
            var random = new Bogus.Randomizer();

            List<Investment> investments = new List<Investment>();

            var randomCount = random.Number(1, 10);

            for (var x = 0; x < randomCount; x++)
            {
                investments.Add(new Investment()
                {
                    Name = random.Word(),

                    Id = random.Long(1, 1000)

                });
            }
            return investments;
        }
    }
}
