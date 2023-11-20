using Microsoft.EntityFrameworkCore;
using InvestmentPerformanceWebAPI.Models;
namespace InvestmentPerformanceWebAPI;

public class InvestmentInfoContext : DbContext
{

    public InvestmentInfoContext(DbContextOptions<InvestmentInfoContext> options)
        : base(options)
    {

    }

    public DbSet<InvestmentInfo> InvestmentInfos { get; set; } = null!;
}