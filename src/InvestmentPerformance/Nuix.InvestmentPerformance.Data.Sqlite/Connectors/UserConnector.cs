using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nuix.InvestmentPerformance.Data.Dtos;
using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Sqlite.Connectors
{
    public class UserConnector : IUserConnector
    {
        private readonly ILogger _logger;
        private readonly InvestmentPerformanceContext _db;

        public UserConnector(ILogger<UserConnector> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _db = _db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<InvestmentPerformanceContext>();
        }
        public IUser GetUser(int id)
        {

            IUser user = null;
            try
            {
                user = _db.Users.FirstOrDefault(u => u.UserId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return user;

        }
    }
}
