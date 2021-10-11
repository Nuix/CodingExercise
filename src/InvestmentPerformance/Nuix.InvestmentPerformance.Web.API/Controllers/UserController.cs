using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nuix.InvestmentPerformance.Data.Dtos;
using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nuix.InvestmentPerformance.Web.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserConnector _userConnector;
        private readonly IInvestmentConnector _investmentConnector;
        private readonly ILogger _logger;
        public UserController(ILogger<UserController> logger, IUserConnector userConnector, IInvestmentConnector investmentConnector)
        {
            _userConnector = userConnector;
            _investmentConnector = investmentConnector;
            _logger = logger;
        }
        [AcceptVerbs("GET")]
        [Route("Users/{id}")]
        public IActionResult GetUser(int id)
        {
            _logger.LogInformation($"GetUser : {id}");
            try
            {
                var user = _userConnector.GetUser(id);

                if (user?.UserId == 0 || user == null)
                {
                    return NotFound();
                }

                return Json(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return StatusCode(500);
        }

        [AcceptVerbs("GET")]
        [Route("Users/{id}/Investments")]
        public IActionResult GetUserInvestments(int id)
        {
            _logger.LogInformation($"GetUserInvestments : {id}");

            try
            {
                var investments = _investmentConnector.GetUserInvestments(id).Select(i=>new InvestListItemDto{InvestmentId=i.InvestmentId,Name=i.Symbol }).ToList();
                return Json(investments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return NotFound();
        }
    }
}
