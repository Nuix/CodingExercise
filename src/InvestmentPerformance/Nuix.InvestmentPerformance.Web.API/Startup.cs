using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle;
using Nuix.InvestmentPerformance.Data.Interfaces;
using Nuix.InvestmentPerformance.Data.Sqlite.Connectors;
using Nuix.InvestmentPerformance.Data.Sqlite;
using Serilog;
using Serilog.Core;
using Microsoft.EntityFrameworkCore;

namespace Nuix.InvestmentPerformance.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(l =>
            {
                l.ClearProviders();
                l.AddSerilog(dispose: true);
            });
            services.AddSingleton(Log.Logger);
            services.AddDbContext<InvestmentPerformanceContext>(options => options.UseSqlite(Configuration.GetConnectionString("cs")));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton<IUserConnector, UserConnector>();
            services.AddSingleton<IInvestmentConnector, InvestmentConnector>();
            services.AddSingleton<IPriceConnector, PriceConnector>();
            services.AddSingleton<IStockConnector, StockConnector>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
