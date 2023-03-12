using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InvestmentPerformanceService : IInvestmentPerformanceService
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IStockInvestmentRepository _stockInvestmentRepository;
        private readonly IStockRepository _stockRepository;

        public InvestmentPerformanceService(IInvestmentRepository investmentRepository, IStockInvestmentRepository stockInvestmentRepository, IStockRepository stockRepository)
        {
            _investmentRepository = investmentRepository;
            _stockInvestmentRepository = stockInvestmentRepository;
            _stockRepository = stockRepository;
        }

        public List<UserInvestment> GetInvestmentsListByUserId(int userId)
        {
            var userInvestments = _investmentRepository.Find(x=>x.UserId== userId);

            return userInvestments.Select(x => new UserInvestment { InvestmentId = x.InvestmentId, InvestmentName = x.InvestmentName }).ToList();
        }

        public List<StockInvestmentDetail> GetStockInvestmentDetails(int investmentId)
        {
            var stockInvestments = _stockInvestmentRepository.Find(x=> x.InvestmentId== investmentId);
            var stockIds = stockInvestments.Select(x => x.StockId);
            var stockSharePrices = _stockRepository.GetStockSharePriceListByIds(stockIds);
            var returnList = new List<StockInvestmentDetail>();

            foreach(var inv in stockInvestments)
            {
                returnList.Add(new StockInvestmentDetail
                {
                    CostBasisPerShare = inv.PricePerShare,
                    NumberOfShares = inv.SharesQuantity,
                    CurrentSharePrice = stockSharePrices[inv.StockId],
                    PurchasedDate= inv.PurchasedDate
                });
            }

            return returnList;
        }
    }
}
