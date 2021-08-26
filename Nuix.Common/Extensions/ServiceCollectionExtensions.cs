using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nuix.Api.Data.Extensions;
using Nuix.Common.Services;
using System;

namespace Nuix.Common.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddNuixCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddNuixDataContexts(configuration);
      services.AddTransient<IInvestmentService, InvestmentService>();

      services.AddTransient<IStockQuoteService, StockQuoteService>();
      services.AddTransient<Lazy<IStockQuoteService>>(provider => new Lazy<IStockQuoteService>(provider.GetService<IStockQuoteService>));
    }
  }
}