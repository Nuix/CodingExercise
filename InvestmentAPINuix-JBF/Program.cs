using InvestmentAPINuix_JBF.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Use Connection String for DB Connection
var connectionString = builder.Configuration.GetConnectionString("InvestmentAPIDatabase");
builder.Services.AddDbContext<InvestmentAPIDatabase>(context => context.UseSqlServer(connectionString));

//Add Controllers

builder.Services.AddScoped<InvestmentAPINuix_JBF.API.InvestmentDataController,InvestmentAPINuix_JBF.API.InvestmentDataController>();
builder.Services.AddScoped<InvestmentAPINuix_JBF.API.UserDataController, InvestmentAPINuix_JBF.API.UserDataController>();

//Add Logging Capabilities
builder.Services.AddLogging();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

//For CORS Issues
builder.Services.AddCors(options => {
    options.AddPolicy("AllowClientOrigin",
    builder => builder.WithOrigins("http://localhost:7043").AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();
app.UseCors("AllowClientOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
