using InvestmentPerformanceWebApi.DbInfrastructure;
using InvestmentPerformanceWebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using InvestmentPerformanceWebApi.Domain.Services;
using InvestmentPerformanceWebApi.Api.Mappings;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));
builder.Services.AddScoped<IInvestmentService, InvestmentService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<InvestmentMappingProfile>();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); //this would need adjusted if we were doing migrations, i dont have a database existing yet so for simplicity im just ensuring created here for it to create and seed the database
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
