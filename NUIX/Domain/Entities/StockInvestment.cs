using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StockInvestment
    {
        public int InvestmentId { get; set; }
        public int StockId { get; set;}
        public DateTime PurchasedDate { get; set; }
        public decimal PricePerShare { get; set; }
        public int SharesQuantity { get; set; }

    }
}
