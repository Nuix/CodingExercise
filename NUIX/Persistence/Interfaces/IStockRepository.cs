using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IStockRepository: IRepository<Stock>
    {
        Dictionary<int, decimal> GetStockSharePriceListByIds(IEnumerable<int> stockIds);
    }
}
