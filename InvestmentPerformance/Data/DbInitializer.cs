using InvestmentPerformance.Models;

namespace InvestmentPerformance.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Models.InvestmentContext context)
        {
            context.Database.EnsureCreated();

            // Look for any stocks.
            if (context.Stocks.Any())
            {
                return;   // DB has been seeded
            }
            var stocks = new Stock[]
            {
                new() { Name = "Apple", TickerSymbol = "AAPL", CurrentPricePerShare = 150.00m, TotalNumberOfShares = 1000000 },
                new() { Name = "Microsoft", TickerSymbol = "MSFT", CurrentPricePerShare = 250.00m, TotalNumberOfShares = 800000 },
                new() { Name = "Google", TickerSymbol = "GOOGL", CurrentPricePerShare = 2800.00m, TotalNumberOfShares = 500000 },
                new() { Name = "Amazon", TickerSymbol = "AMZN", CurrentPricePerShare = 3400.00m, TotalNumberOfShares = 300000 },
                new() { Name = "Tesla", TickerSymbol = "TSLA", CurrentPricePerShare = 700.00m, TotalNumberOfShares = 600000 }
            };
            foreach (Models.Stock s in stocks)
            {
                context.Stocks.Add(s);
            }
            context.SaveChanges();
            var investments = new Investment[]
            {
                new() { UserID = 1, StockID = stocks[0].ID, SharesOwned = 50, CostBasisPerShare = 120.00m, PurchaseDate = DateTime.Parse("2021-01-15") },
                new() { UserID = 1, StockID = stocks[1].ID, SharesOwned = 30, CostBasisPerShare = 200.00m, PurchaseDate = DateTime.Parse("2025-06-22") },
                new() { UserID = 2, StockID = stocks[2].ID, SharesOwned = 10, CostBasisPerShare = 2500.00m, PurchaseDate = DateTime.Parse("2024-09-10") },
                new() { UserID = 2, StockID = stocks[3].ID, SharesOwned = 5, CostBasisPerShare = 3200.00m, PurchaseDate = DateTime.Parse("2021-09-05") },
                new() { UserID = 3, StockID = stocks[4].ID, SharesOwned = 20, CostBasisPerShare = 600.00m, PurchaseDate = DateTime.Parse("2021-11-11") }
            };
            foreach (Models.Investment i in investments)
            {
                context.Investments.Add(i);
            }
            context.SaveChanges();
        }
    }
}
