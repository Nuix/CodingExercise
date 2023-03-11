using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Stock
    {
        public int StockId { get; set; }
        public string StockName { get; set; }
        public decimal SharePrice { get; set; }// Should reflect the latest share price as a given date and time.
    }
}
