using InvestmentPerformanceDomain;
using InvestmentPerformanceDomain.Data;
using InvestmentPerformanceDomain.Repository;
using InvestmentPerformanceWebApi.Configurations;
using InvestmentPerformanceWebApi.Services;
using InvestmentPerformanceWebApi.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger and Versioning
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.Configure<PricingConfiguration>(builder.Configuration.GetSection(PricingService.PRICING_HTTPCLIENTNAME));

// HTTP Clients
builder.Services.AddHttpClient("Pricing", c =>
{
    c.BaseAddress = new Uri("https://www.alphavantage.co");
});

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHoldingService, HoldingService>();
builder.Services.AddScoped<IPricingService, PricingService>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHoldingRepository, HoldingRepository>();

// Database
builder.Services.AddDbContext<InvestmentPerformanceContext>(options =>
                options.UseInMemoryDatabase("InvestmentPerformance"));

// ------------------------------------------------------------------------------------------------------------
// App Configure
// ------------------------------------------------------------------------------------------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});

// Initial database data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<InvestmentPerformanceContext>();
        InvestmentPerformanceInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
