using Application.ConcreteObjects;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class InvestmentFactory
    {
        private readonly IStockInvestmentRepository _stockInvestmentRepository;
        private readonly IStockRepository _stockRepository;

        public InvestmentFactory(IStockInvestmentRepository stockInvestmentRepository, IStockRepository stockRepository)
        {
            _stockInvestmentRepository = stockInvestmentRepository;
            _stockRepository = stockRepository;
        }

        public IInvestmentDetail? GetInvestmentDetailInstance(InvestmentTypes investmentType)
        {
            switch (investmentType)
            {
                case InvestmentTypes.Stock:
                    var instance = new StockDeTail(_stockInvestmentRepository, _stockRepository);
                    return instance;

                case InvestmentTypes.Bond:
                    return new BondDetail();

                case InvestmentTypes.MutualFund:
                    return new MutualFundDetail();

                default:
                    return null;
            }
        }
    }
}
