using System.Globalization;
using InvestmentPerformance.Api.Data;
using InvestmentPerformance.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Api.Api;

public static class InvestmentEndpoints
{
    public static void MapInvestmentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/users/{userId}/investments").WithTags("Investments");
        group.MapGet("", ListAsync)
            .WithName("ListInvestments")
            .WithSummary("List a demo user's current investments")
            .Produces<List<InvestmentSummary>>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound);
        group.MapGet("/{investmentId}", DetailsAsync)
            .WithName("GetInvestmentPerformance")
            .WithSummary("Get a demo investment's value, holding term and unrealized gain or loss")
            .Produces<InvestmentDetails>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> ListAsync(string userId, InvestmentsDbContext db, CancellationToken cancellationToken)
    {
        if (!TryId(userId, out var id))
        {
            return InvalidId("userId");
        }

        if (!await db.Users.AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken))
        {
            return Results.Problem(statusCode: 404, title: "User not found.");
        }

        var investments = await db.Investments.AsNoTracking()
            .Where(x => x.UserId == id).OrderBy(x => x.Id)
            .Select(x => new InvestmentSummary(x.Id, x.Name)).ToListAsync(cancellationToken);
        return Results.Ok(investments);
    }

    private static async Task<IResult> DetailsAsync(string userId, string investmentId,
        InvestmentsDbContext db, TimeProvider clock, CancellationToken cancellationToken)
    {
        if (!TryId(userId, out var ownerId))
        {
            return InvalidId("userId");
        }
        if (!TryId(investmentId, out var id))
        {
            return InvalidId("investmentId");
        }

        var investment = await db.Investments.AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == ownerId && x.Id == id, cancellationToken);
        if (investment is null)
        {
            return Results.Problem(statusCode: 404, title: "Investment not found.");
        }

        return Results.Ok(PerformanceCalculator.Calculate(investment,
            DateOnly.FromDateTime(clock.GetUtcNow().UtcDateTime)));
    }

    private static bool TryId(string value, out int id) =>
        int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out id) && id > 0;

    private static IResult InvalidId(string name) => Results.ValidationProblem(
        new Dictionary<string, string[]> { [name] = ["Must be a positive 32-bit integer."] });
}
