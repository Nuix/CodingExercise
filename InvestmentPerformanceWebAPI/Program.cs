using InvestmentPerformanceWebAPI.Database.Contexts;
using InvestmentPerformanceWebAPI.Interfaces;
using InvestmentPerformanceWebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core InMemory DB for this exercise
builder.Services.AddDbContext<InvestmentDbContext>(options =>
    options.UseInMemoryDatabase("InvestmentsDb"));

// Register the application service responsible for investment-related business logic.
builder.Services.AddScoped<IUserInvestmentService, UserInvestmentService>();

var app = builder.Build();

// Force database creation & seeding
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InvestmentDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
