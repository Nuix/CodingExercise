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
                .ForMember(dest => dest.CurrentValue, opt => opt.MapFrom(src => src.SharesOwned * src.Stock.CurrentPricePerShare))
                .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.Stock.CurrentPricePerShare))
                .ForMember(dest => dest.TotalGainOrLoss, opt => opt.MapFrom(src => (src.SharesOwned * src.Stock.CurrentPricePerShare) - (src.SharesOwned * src.CostBasisPerShare) ))
                .ForMember(dest => dest.Term, opt => opt.MapFrom(src => (DateTime.Now - src.PurchaseDate).TotalDays > 365 ? "Long-Term" : "Short-Term"));

        }
    }
}
