using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nuix_Project.APIObjects;
using Nuix_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nuix_Project.Controllers
{
    [ApiController]

    public class InvestmentController : ControllerBase
    {

        private readonly ILogger<InvestmentController> _logger;
        private readonly IDataLayer _dataLayer;
        private readonly ILoginLayer _loginLayer;

        public InvestmentController(ILogger<InvestmentController> logger, IDataLayer dataLayer, ILoginLayer loginLayer)
        {
            _loginLayer = loginLayer;
            _logger = logger;
            _dataLayer = dataLayer;
        }

        [HttpGet]
        [Route("GetInvestmentsByUserId")]
        public List<Investment> GetInvestmentsByUserId([FromRoute]string token, [FromRoute] long id)
        {

            try
            {

                if (!_loginLayer.ValidateToken(token))

                    throw new Exception("Invalid Token");

                List<Investment> investment = _dataLayer.GetInvestmentsByUserId(id);

                return investment;

            }
            catch (Exception e)
            {

                _logger.LogError(e, "GetInvestmentsByUserId Failed");

            }
            return null;
        }


        [HttpGet]
        [Route("GetInvestmentDetailsByInvestmentId")]
        public InvestmentDetails GetInvestmentDetailsByInvestmentId([FromRoute] string token, [FromRoute] long id)
        {
            try
            {

                if (!_loginLayer.ValidateToken(token))

                    throw new Exception("Invalid Token");

                InvestmentDetails investmentDetails = _dataLayer.GetInvestmentDetailsByInvestmentId(id);

                return investmentDetails;

            }
            catch (Exception e)
            {

                _logger.LogError(e, "GetInvestmentDetailsByInvestmentId Failed");

                throw new Exception("Failed to Retrieve Investment Details");
            }
        }
    }
}
