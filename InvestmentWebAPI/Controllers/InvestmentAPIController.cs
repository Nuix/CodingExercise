using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InvestmentWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestmentAPIController : ControllerBase
    {
        private readonly ILogger<InvestmentAPIController> _logger;
        private readonly IDbConnection _connection;
        public InvestmentAPIController(ILogger<InvestmentAPIController> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        [HttpGet(Name = "GetUserInvestments", Order = 1)]
        public InvestmentAPI.UsersInvestments GetUserInvestments([FromRoute] string userId)
        {
            var investmentAPI = new InvestmentAPI.InvestmentAPI(_connection, _logger);
            Guid.TryParse(userId, out Guid userGuid);
            if (userGuid == Guid.Empty) 
            {
                return null;
            }
            return investmentAPI.GetUserInvestments(userGuid);
        }

        [HttpGet(Name = "GetInvestmentDetails", Order = 2)]
        public InvestmentAPI.InvestmentRecord GetInvestmentDetail([FromRoute] string investmentId)
        {
            var investmentAPI = new InvestmentAPI.InvestmentAPI(_connection, _logger);
            Guid.TryParse(investmentId, out Guid investmentGuid);
            if (investmentGuid == Guid.Empty)
            {
                return null;
            }
            return investmentAPI.GetInvestmentRecord(investmentGuid);
        }
    }
}
