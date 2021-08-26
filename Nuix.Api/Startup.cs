using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nuix.Common.Extensions;
using System;
using Microsoft.Extensions.Logging;
using Nuix.Api.Controllers;
using Microsoft.OpenApi.Models;

namespace Nuix.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Investment Performance Web API - Coding Exercise",
          Description = "ASP.NET Core/EF Core/SQL Server",
          Contact = new OpenApiContact
          {
            Name = "Jeromy Dean",
            Email = "jeromydean@gmail.com"
          }
        });
      });

      services.AddSwaggerGen();

      services.AddNuixCommonServices(Configuration);

      services.AddTransient<Lazy<ILogger<InvestmentsController>>>(provider => new Lazy<ILogger<InvestmentsController>>(provider.GetService<ILogger<InvestmentsController>>));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nuix API");
      });

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}