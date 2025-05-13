using NUIX.InvestmentPerformance.API.Data;
using NUIX.InvestmentPerformance.API.Middleware;
using NUIX.InvestmentPerformance.API.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register investment service for dependency injection
builder.Services.AddScoped<IInvestmentService, InvestmentService>();

builder.Services.AddDbContext<InvestmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Enable middleware to serve Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
