using Domain.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class InvestmentRepository : Repository<Investment>, IInvestmentRepository
    {
        public InvestmentRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
