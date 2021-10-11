using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nuix.InvestmentPerformance.Data.Dtos;
using Nuix.InvestmentPerformance.Data.Interfaces;
using Nuix.InvestmentPerformance.Data.Sqlite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuix.InvestmentPerformance.Data.Sqlite.Connectors
{
    public class InvestmentConnector : IInvestmentConnector
    {

        private readonly ILogger _logger;
        private readonly InvestmentPerformanceContext _db;

        public InvestmentConnector(ILogger<InvestmentConnector> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<InvestmentPerformanceContext>();
        }
        public IInvestment GetInvestment(int investmentId)
        {
            IInvestment investment = null;
            try
            {
                investment = _db.Investments.FirstOrDefault(i => i.InvestmentId == investmentId);
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return investment;

        }

        public IEnumerable<IInvestment> GetUserInvestments(int userId)
        {
            List<Investment> investments = null;
            try
            {
                
                investments = _db.Investments.Where(i => i.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return investments;
        }

    }
}
