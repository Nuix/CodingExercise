using CodingExercise.Data;
using CodingExercise.Models;
using CodingExercise.Repositories;

namespace CodingExercise.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestmentRepository _investmentRepository;
        public InvestmentService(CodingExerciseContext context) 
        {
            _investmentRepository = new InvestmentRepository(context);
        }

        public Investment GetInvestment(int investmentId)
        {
            return _investmentRepository.GetInvestment(investmentId);
        }

        public List<StockInvestment> GetStockInvestmentsForUser(int userId)
        {
            return _investmentRepository.GetStockInvestmentsForUser(userId);
        }
    }
}
