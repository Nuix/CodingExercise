using InvestmentPerformanceApi.Data;
using InvestmentPerformanceApi.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace InvestmentPerformanceApi.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly InvestmentPerformanceContext _context;

        public InvestmentService(InvestmentPerformanceContext context)
        {
            _context = context;
        }

        public async Task<string?> getUserInvestmentsAsync(int userId)
        {

            var dbUser = await _context.Users.FindAsync(userId);

            if (dbUser == null)
            {
                return null;
            }

            var query = from user in _context.Users
                        join investment in _context.Investments on user.UserId equals investment.UserId
                        where user.UserId == userId
                        select new
                        {
                            UserId = user.UserId,
                            UserName = user.UserName,
                            InvestmentId = investment.InvestmentId,
                            StockName = investment.StockName
                        };

            var result = query.ToList();

            var responseList = new List<object>();

            foreach (var item in result)
            {
                var existingUser = responseList.FirstOrDefault(x => (int)x.GetType().GetProperty("UserId").GetValue(x) == item.UserId);

                if (existingUser == null)
                {
                    var responseObj = new
                    {
                        UserId = item.UserId,
                        UserName = item.UserName,
                        Investments = new List<object>
                        {
                            new
                            {
                                InvestmentId = item.InvestmentId,
                                StockName = item.StockName
                            }
                        }
                    };
                    responseList.Add(responseObj);
                }
                else
                {
                    var investmentsList = (List<object>)existingUser.GetType().GetProperty("Investments").GetValue(existingUser);
                    investmentsList.Add(new
                    {
                        InvestmentId = item.InvestmentId,
                        StockName = item.StockName
                    });
                }
            }

            // this means we didn't find any investments for the user. Just return the user information and an empty investments list.
            if(responseList.Count == 0)
            {
                var responseObj = new
                {
                    UserId = dbUser.UserId,
                    UserName = dbUser.UserName,
                    Investments = new List<object> { }
                };
                var jsonResponseNoInvestments = JsonConvert.SerializeObject(responseObj, Formatting.Indented);
                return jsonResponseNoInvestments;
            }

            var jsonResponse = JsonConvert.SerializeObject(responseList, Formatting.Indented);
            return jsonResponse;
        }

        public async Task<string?> getInvestmentDetails(int userId, int investmentId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }

            var investment = await _context.Investments.FindAsync(investmentId);
            if (investment == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(investment, Formatting.Indented);
            
        }
    }
}
