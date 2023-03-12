using Application.Dtos;
using Application.Interfaces;
using Persistence.Interfaces;

namespace Application.ConcreteObjects
{
    public class StockDeTail : IInvestmentDetail
    {
        private readonly IStockInvestmentRepository _stockInvestmentRepository;
        private readonly IStockRepository _stockRepository;
        public StockDeTail(IStockInvestmentRepository stockInvestmentRepository, IStockRepository stockRepository)
        {

            _stockInvestmentRepository = stockInvestmentRepository;
            _stockRepository = stockRepository;
        }


        public List<object> GetInvestmentDetails(int investmentId)
        {
            var stockInvestments = _stockInvestmentRepository.Find(x => x.InvestmentId == investmentId);
            var stockIds = stockInvestments.Select(x => x.StockId);
            var stockSharePrices = _stockRepository.GetStockSharePriceListByIds(stockIds);
            var returnList = new List<object>();

            foreach (var inv in stockInvestments)
            {
                returnList.Add(new StockInvestmentDetail
                {
                    CostBasisPerShare = inv.PricePerShare,
                    NumberOfShares = inv.SharesQuantity,
                    CurrentSharePrice = stockSharePrices[inv.StockId],
                    PurchasedDate = inv.PurchasedDate
                });
            }

            return returnList;
        }
    }
}
