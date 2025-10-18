using System.Text.Json;
using InvestmentApi.Data;
using InvestmentApi.Models; // SeedData class
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InvestmentsDb"));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- Seeding from JSON (controlled by env vars) ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // --- NEW CONFIG-BASED CODE ---
    var cfg = app.Configuration.GetSection("Seed");
    var seedOnStartup = cfg.GetValue("OnStartup", true);
    var seedFile = cfg.GetValue("File", "data.json");
    var path = Path.IsPathRooted(seedFile)
        ? seedFile
        : Path.Combine(app.Environment.ContentRootPath, seedFile);
    // --- END CONFIG BLOCK ---

    if (seedOnStartup && !db.Users.Any())
    {      

        if (File.Exists(path))
        {
            var json = await File.ReadAllTextAsync(path);
            var seed = JsonSerializer.Deserialize<SeedData>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (seed != null)
            {
                db.Users.AddRange(seed.Users);
                db.Investments.AddRange(seed.Investments);
                await db.SaveChangesAsync();
            }
        }
        else
        {
            app.Logger.LogWarning("Seed file not found at {Path}", path);
        }
    }
}
// --- end seeding ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
