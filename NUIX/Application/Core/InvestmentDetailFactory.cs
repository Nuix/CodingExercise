using Application.ConcreteObjects;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Core
{
    public class InvestmentDetailFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public InvestmentDetailFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public IInvestmentDetail? GetInvestmentDetailInstance(InvestmentTypes investmentType)
        {
            switch (investmentType)
            {
                case InvestmentTypes.Stock:
                    return _serviceProvider.GetService<StockDetail>();

                case InvestmentTypes.Bond:
                    return _serviceProvider.GetService<BondDetail>();

                case InvestmentTypes.MutualFund:
                    return _serviceProvider.GetService<MutualFundDetail>();

                default:
                    throw new NotImplementedException($"Investment type not implemented: {investmentType}");
            }
        }
    }
}
