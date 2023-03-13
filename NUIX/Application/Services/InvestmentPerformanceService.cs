using Application.Core;
using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using Persistence.Interfaces;
using System.Net.Http;

namespace Application.Services
{
    public class InvestmentPerformanceService : IInvestmentPerformanceService
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly InvestmentDetailFactory _investmentFactory;

        public InvestmentPerformanceService(IInvestmentRepository investmentRepository, InvestmentDetailFactory investmentFactory)
        {
            _investmentRepository = investmentRepository;
            _investmentFactory = investmentFactory;
        }

        public List<UserInvestment> GetInvestmentsListByUserId(int userId)
        {
            var userInvestments = _investmentRepository.Find(x=>x.UserId== userId);

            return userInvestments.Select(
                                            x => new UserInvestment 
                                            { 
                                                InvestmentId = x.InvestmentId, 
                                                InvestmentName = x.InvestmentName 
                                            }).ToList();
        }

        public List<object> GetInvestmentDetailsById(int investmentId)
        {
            var investmentType = _investmentRepository.GetInvestmentTypeId(investmentId);
            var objectInstance = _investmentFactory.GetInvestmentDetailInstance((InvestmentTypes)investmentType);

            return objectInstance.GetInvestmentDetails(investmentId);
        }
    }
}
