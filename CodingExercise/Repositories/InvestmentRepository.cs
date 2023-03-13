using CodingExercise.Data;
using CodingExercise.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CodingExercise.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private CodingExerciseContext _context;

        public InvestmentRepository(CodingExerciseContext context) 
        {
            _context = context;
        }

        public Investment GetInvestment(int investmentId)
        {
            var query = $"SELECT i.id, i.StockId, i.UserId, i.Quantity, i.CostBasis, i.AcquiredDate, s.Price " +
                $"FROM Investments i, Stocks s " +
                $"WHERE s.Id=i.StockId AND i.Id=@investmentId";

            var investment = _context.Investments.FromSqlRaw(query, new SqlParameter("@investmentId", investmentId)).Include(i => i.Stock).FirstOrDefault();
            return investment;
        }

        public List<StockInvestment> GetStockInvestmentsForUser(int userId)
        {
            // var query = $"SELECT i.Id AS InvestmentId, s.Name AS StockName FROM Investments i, Stocks s WHERE s.Id = i.StockId AND i.UserId = @userId";

            var stockInvestments = _context.Users.Where(u => u.Id == userId).Include(u => u.Investments).ThenInclude(i => i.Stock)
                .Select(u => u.Investments.Select(i => new StockInvestment() { StockName = i.Stock.Name, InvestmentId = i.Id })).FirstOrDefault().ToList();
            return stockInvestments;
        }
    }
}
