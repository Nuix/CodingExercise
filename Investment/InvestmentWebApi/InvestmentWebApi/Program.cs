using InvestmentWebApi.Data.Api;
using InvestmentWebApi.Data.Impl;
using InvestmentWebApi.Services.Api;
using InvestmentWebApi.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies for injection
builder.Services.AddSingleton<IDbReader, SqliteDbReader>();
builder.Services.AddScoped<ICurrentTimeProvider, CurrentTimeProvider>();
builder.Services.AddScoped<IPortfolioManager, PortfolioManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();