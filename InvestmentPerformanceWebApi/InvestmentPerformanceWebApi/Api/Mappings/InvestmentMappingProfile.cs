using AutoMapper;
using InvestmentPerformanceWebApi.Api.ViewModels.Investments;
using InvestmentPerformanceWebApi.Domain.Models;

namespace InvestmentPerformanceWebApi.Api.Mappings
{
    public class InvestmentMappingProfile : Profile
    {
        public InvestmentMappingProfile()
        {
            CreateMap<Investment, InvestmentDetailView>();
            CreateMap<Investment, InvestmentSummaryView>();
        }
    }
}
