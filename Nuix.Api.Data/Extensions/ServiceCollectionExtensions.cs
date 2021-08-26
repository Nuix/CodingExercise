using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nuix.Api.Data.Contexts;
using Microsoft.Extensions.Configuration;

namespace Nuix.Api.Data.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddNuixDataContexts(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<NuixContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
      services.AddScoped<INuixContext>(provider => provider.GetService<NuixContext>());
    }
  }
}