using Microsoft.EntityFrameworkCore;
using InvestmentPerformanceWebAPI.Models;
namespace InvestmentPerformanceWebAPI;

public class InvestorInfoContext : DbContext
{

    public InvestorInfoContext(DbContextOptions<InvestorInfoContext> options)
        : base(options)
    {

    }

    public DbSet<InvestmentInfo> InvestorInfos { get; set; } = null!;

    public DbSet<Models.InvestorInfo> InvestorInfo { get; set; } = default!;
}