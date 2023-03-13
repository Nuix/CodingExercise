using CodingExercise.Models;

namespace CodingExercise.Repositories
{
    public interface IInvestmentRepository
    {
        public Investment GetInvestment(int investmentId);
        public List<StockInvestment> GetStockInvestmentsForUser(int userId);
    }
}
