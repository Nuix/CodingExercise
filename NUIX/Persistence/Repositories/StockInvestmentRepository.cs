using Domain.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class StockInvestmentRepository : Repository<StockInvestment>, IStockInvestmentRepository
    {
        public StockInvestmentRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
