using Domain.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(DataBaseContext context) : base(context)
        {
        }

        public Dictionary<int, decimal> GetStockSharePriceListByIds(IEnumerable<int> stockIds)
        {
            var result = (from s in _context.Stocks
                         join i in stockIds
                         on s.StockId equals i
                         select s).ToDictionary(y => y.StockId, z => z.SharePrice);

            return result;
        }
    }
}
