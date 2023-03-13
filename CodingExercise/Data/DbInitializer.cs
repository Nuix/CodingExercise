using CodingExercise.Models;

namespace CodingExercise.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CodingExerciseContext context)
        {
            // If data exists, don't seed
            if (context.Users.Any())
            {
                return;
            }

            var stocks = new Stock[]
            {
                new Stock(name: "Fellowship", price: 100),
                new Stock(name: "Twin Towers", price: 65),
                new Stock(name: "Return of the King", 250.10)
            };

            context.Stocks.AddRange(stocks);
            context.SaveChanges();

            var investments = new Investment[]
            {
                new Investment(stock: stocks[0], quantity: 2),
                new Investment(stock: stocks[1], quantity: 10),
                new Investment(stock: stocks[2], quantity: 5)
            };

            context.Investments.AddRange(investments);
            context.SaveChanges();

            var users = new User[]
            {
                new User { Investments = new List<Investment>() { investments[0], investments[1] } },
                new User { Investments = new List<Investment>() { investments[2] } },
                new User { }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
