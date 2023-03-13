using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StockInvestment
    {
        [ForeignKey(nameof(Investment))]
        public int InvestmentId { get; set; }

        [ForeignKey(nameof(Stock))]
        public int StockId { get; set;}
        public DateTime PurchasedDate { get; set; }
        
        [Precision(18, 2)]
        public decimal PricePerShare { get; set; }
        public int SharesQuantity { get; set; }

    }
}
