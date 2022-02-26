using Nuix_Project.APIObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nuix_Project.Data
{
    public interface IDataLayer
    {
        public InvestmentDetails GetInvestmentDetailsByInvestmentId(long id);

        public List<Investment> GetInvestmentsByUserId(long id);
    
    }
}
