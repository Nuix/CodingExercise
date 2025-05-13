namespace NUIX.InvestmentPerformance.API.Models
{
    public class UserInvestment
    {
        public Guid UserInvestmentID { get; set; }
        public List<Investment> UserInvestments { get; set; }
    }
}
