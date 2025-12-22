
namespace InvestmentPerformanceWebApi.DbInfrastructure.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreatedUtc { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
        public ICollection<Investment> Investments { get; set; } = null!;

    }
}
