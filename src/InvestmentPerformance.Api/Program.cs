using System.Diagnostics;
using InvestmentPerformance.Api.Api;
using InvestmentPerformance.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

if (args is ["--check-health"])
{
    using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
    try
    {
        using var response = await client.GetAsync("http://127.0.0.1:8080/health");
        Environment.ExitCode = response.IsSuccessStatusCode ? 0 : 1;
    }
    catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
    {
        Environment.ExitCode = 1;
    }
    return;
}

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddJsonConsole();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddDbContext<InvestmentsDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("Investments") ?? "Data Source=data/investments.db"));
builder.Services.AddProblemDetails(options => options.CustomizeProblemDetails = context =>
    context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier);
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddOpenApi(options => options.AddDocumentTransformer((document, _, _) =>
{
    document.Info = new OpenApiInfo
    {
        Title = "Investment Performance API",
        Version = "v1",
        Description = "Read-only demonstration using synthetic users and fixed price snapshots. " +
            "User 1 has investments 1, 2 and 3; user 2 has investment 4; user 3 has no investments. " +
            "All values use USD. Each investment represents one purchase lot. No authentication is configured."
    };
    return Task.CompletedTask;
}));

var app = builder.Build();
app.Use(async (context, next) =>
{
    var timer = Stopwatch.StartNew();
    try
    {
        await next(context);
    }
    finally
    {
        app.Logger.LogInformation("HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms. TraceId: {TraceId}",
            context.Request.Method, context.Request.Path, context.Response.StatusCode,
            timer.ElapsedMilliseconds, context.TraceIdentifier);
    }
});
app.UseExceptionHandler();
app.UseStatusCodePages();

// One application instance owns this local database. A deployment runs migrations before serving requests.
var connectionString = app.Configuration.GetConnectionString("Investments") ?? "Data Source=data/investments.db";
var dataSource = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder(connectionString).DataSource;
if (!string.IsNullOrEmpty(dataSource) && dataSource != ":memory:")
{
    var directory = Path.GetDirectoryName(Path.GetFullPath(dataSource));
    Directory.CreateDirectory(directory!);
}
await using (var scope = app.Services.CreateAsyncScope())
{
    await DemoData.InitializeAsync(scope.ServiceProvider.GetRequiredService<InvestmentsDbContext>(), app.Lifetime.ApplicationStopping);
}

app.MapInvestmentEndpoints();
app.MapGet("/health", async (InvestmentsDbContext db, CancellationToken cancellationToken) =>
    await db.Database.CanConnectAsync(cancellationToken)
        ? Results.Ok(new { status = "healthy" })
        : Results.Problem(statusCode: 503, title: "Database unavailable."))
    .ExcludeFromDescription();
app.MapOpenApi();
app.MapScalarApiReference("/docs", options => options.WithTitle("Investment Performance API"));
app.MapGet("/", () => Results.Redirect("/docs")).ExcludeFromDescription();
app.Run();

public partial class Program;
