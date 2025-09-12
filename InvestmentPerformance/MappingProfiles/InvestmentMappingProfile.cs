using AutoMapper;
using InvestmentPerformance.Models;
using InvestmentPerformance.DTOs;

namespace InvestmentPerformance.MappingProfiles
{
    public class InvestmentMappingProfile : Profile
    {
        public InvestmentMappingProfile()
        {
            CreateMap<Investment, InvestmentDto>()
                .ForMember(dest => dest.StockName, opt => opt.MapFrom(src => src.Stock.Name));
            CreateMap<Investment, InvestmentDetailsDto>()
                .ForMember(dest => dest.CurrentValue, opt => opt.MapFrom(investment => investment.SharesOwned * investment.Stock.CurrentPricePerShare))
                .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(investment => investment.Stock.CurrentPricePerShare))
                .ForMember(dest => dest.TotalGainOrLoss, opt => opt.MapFrom(investment => (investment.SharesOwned * investment.Stock.CurrentPricePerShare) - (investment.SharesOwned * investment.CostBasisPerShare) ))
                .ForMember(dest => dest.Term, opt => opt.MapFrom(investment => (DateTime.Now - investment.PurchaseDate).TotalDays > 365 ? "Long-Term" : "Short-Term"));

        }
    }
}
