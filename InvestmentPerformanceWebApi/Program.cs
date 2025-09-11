using InvestmentPerformanceWebApi.Data;
using InvestmentPerformanceWebApi.Models;
using InvestmentPerformanceWebApi.Repositories;
using InvestmentPerformanceWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<InvestmentContext>();
builder.Services.AddSingleton<IInvestmentRepository, InMemoryInvestmentRepository>();
builder.Services.AddScoped<IInvestmentService, InvestmentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors("_allowLocalOrigins");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.MapControllers();

// Seed demo data
SeedData(app.Services);

app.Run();
return;

static void SeedData(IServiceProvider services)
{
    var ctx = services.GetRequiredService<InvestmentContext>();

    if (ctx.Investments.Count != 0) return;
    
    ctx.Investments.AddRange([
        new Investment
        {
            Id = 1,
            UserId = 1001,
            Name = "Nuix",
            NumberOfShares = 50m,
            CostBasisPerShare = 100m,
            CurrentPrice = 120m,
            PurchaseDate = DateTime.UtcNow.AddMonths(-6)
        },
        new Investment
        {
            Id = 2,
            UserId = 1001,
            Name = "Google",
            NumberOfShares = 200m,
            CostBasisPerShare = 10m,
            CurrentPrice = 9.5m,
            PurchaseDate = DateTime.UtcNow.AddYears(-2)
        },
        new Investment
        {
            Id = 3,
            UserId = 1002,
            Name = "Amazon",
            NumberOfShares = 10m,
            CostBasisPerShare = 300m,
            CurrentPrice = 350m,
            PurchaseDate = DateTime.UtcNow.AddMonths(-3)
        }
    ]);
}