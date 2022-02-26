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

    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ILoginLayer _loginLayer;

        public LoginController(ILogger<LoginController> logger, ILoginLayer loginLayer)
        {
            _logger = logger;
            _loginLayer = loginLayer;
        }

        [HttpGet]
        [Route("GetToken")]
        public string GetToken([FromRoute] string username, string password)
        { 
            try
            {
                _logger.LogInformation("User " + username + " attempted login.");
                string token = _loginLayer.GetLoginToken(username, password);


                if(token != null)
                {

                    return token;

                }
                else
                {

                    _logger.LogError("User " + username + " Failed to Login");

                    throw new Exception("Invalid Login");

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Call Failed");
            }
            return null;
        }
    }
}
