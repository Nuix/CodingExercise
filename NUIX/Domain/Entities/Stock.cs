using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockId { get; set; }
        public string StockName { get; set; }

        [Precision(18, 2)]
        public decimal SharePrice { get; set; }// Should reflect the latest share price as a given date and time.
    }
}
