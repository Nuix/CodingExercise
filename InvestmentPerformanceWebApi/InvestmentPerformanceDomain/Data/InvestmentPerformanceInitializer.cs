using InvestmentPerformanceDomain.Models;

namespace InvestmentPerformanceDomain.Data;

public static class InvestmentPerformanceInitializer
{
    public static void Initialize(InvestmentPerformanceContext context)
    {
        context.Database.EnsureCreated();


        if (!context.Users.Any())
        {
            context.Users.Add(new User { Id = 1, Name = "Tom Smith" });
            context.Users.Add(new User { Id = 2, Name = "Frank Jones" });
            context.SaveChanges();
        }

        if (!context.Holdings.Any())
        {
            context.Holdings.Add(new Holding { UserId = 1, HoldingType = InvestmentType.Stock, Symbol = "IBM", Name = "INTERNATIONAL BUSINESS MACHINES CORPORATION", Cost = 111.12m, Amount = 100, OpenDate = new DateTime(2020, 2, 23) });
            context.Holdings.Add(new Holding { UserId = 1, HoldingType = InvestmentType.Stock, Symbol = "DKS", Name = "DICK'S Sporting Goods Inc", Cost = 109.76m, Amount = 200, OpenDate = new DateTime(2022, 1, 10) });
            context.Holdings.Add(new Holding { UserId = 1, HoldingType = InvestmentType.Stock, Symbol = "DELL", Name = "DELL TECHNOLOGIES INC", Cost = 49.13m, Amount = 100, OpenDate = new DateTime(2021, 8, 18) });
            context.Holdings.Add(new Holding { UserId = 1, HoldingType = InvestmentType.Stock, Symbol = "DIS", Name = "WALT DISNEY CO", Cost = 168.27m, Amount = 100, OpenDate = new DateTime(2020, 5, 19) });


            context.Holdings.Add(new Holding { UserId = 2, HoldingType = InvestmentType.Bond, Symbol = "64971Q7G2", Name = "New York New York City Transitional Taxable Subordinated Future Tax Secured", Cost = 101.75m, Amount = 100, OpenDate = new DateTime(2020, 2, 23) });
            context.Holdings.Add(new Holding { UserId = 2, HoldingType = InvestmentType.Bond, Symbol = "64711N6L4", Name = "New Mexico Finance Authority Revenue Senior Lien Public Project Revolving Fund", Cost = 109.842m, Amount = 200, OpenDate = new DateTime(2022, 1, 10) });
            context.Holdings.Add(new Holding { UserId = 2, HoldingType = InvestmentType.MutualFunds, Symbol = "TGBAX", Name = "Templeton Global Bond Fund", Cost = 9.36m, Amount = 100, OpenDate = new DateTime(2021, 4, 15) });
            context.Holdings.Add(new Holding { UserId = 2, HoldingType = InvestmentType.MutualFunds, Symbol = "SBBAX", Name = "Franklin Multi - Asset Conservative Growth Fund", Cost = 15.81m, Amount = 100, OpenDate = new DateTime(2020, 9, 22) });
            context.SaveChanges();
        }

    }
}
