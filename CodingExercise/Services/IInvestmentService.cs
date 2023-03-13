using CodingExercise.Models;
using CodingExercise.Repositories;

namespace CodingExercise.Services
{
    public interface IInvestmentService
    {
        public Investment GetInvestment(int investmentId);
        public List<StockInvestment> GetStockInvestmentsForUser(int userId);
    }
}
