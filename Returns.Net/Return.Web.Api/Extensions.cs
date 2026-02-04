using System;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Return.Web.Api.Services;
using Returns.Models;

namespace Return.Web.Api;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        //public IServiceCollection AddReturnsDbContextFactory(Func<IServiceProvider, DbContextOptions>? factory = null)
        //{
        //    //Would setup ef based on Func or existing database options, but will keep simplistic for now and
        //    //  use hardcoded data in the service
        //    return services;
        //}
        
        public IServiceCollection AddReturnsService()
        {
            //if the service required a more complex setup, we could add options and validation here. 
            // Say you need to retrieve keys from vault for the 
            services.AddSingleton<IShareService, DumbyShareService>();
            services.AddSingleton<IInvestmentService, InvestmentService>();
            return services;
        }
        
        public IServiceCollection AddMapsterProfiles()
        {
            var config = new TypeAdapterConfig();
            //usually, mapping from db models to response models, use to using mapster or manual mapping
            config.NewConfig<Database.Investment, InvestmentRes>()
                .ConstructUsing(src => new(src.Id, src.UserId, src.ShareName, src.ShareCount, src.CostBasisPerShare, src.PurchaseDateTime, 0m)); 
            // CurrentPrice will be updated via injected share service
                
            config.Compile(); //precompile
            services.AddSingleton(config);
            services.AddSingleton<IMapper>(sp => new Mapper(config));
            return services;
        }
    }
}