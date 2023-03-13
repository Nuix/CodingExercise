using EnumsNET;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodingExercise.Models
{
    [PrimaryKey(nameof(Id))]
    public class Investment
    {
        public Investment() { }

        public Investment(Stock stock, int quantity)
        {
            AcquiredDate = DateTime.UtcNow;
            CostBasis = stock.Price;
            Stock = stock;
            Quantity = quantity;
        }

        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        [JsonIgnore]
        public DateTime AcquiredDate { get; set; }
        public double CostBasis { get; set; }
        [JsonIgnore]
        public Stock Stock { get; set; }
        public int Quantity { get; set; }

        // Non-db properties
        public double Value
        {
            get
            {
                return Quantity * CurrentPrice;
            }
        }

        public double Returns
        {
            get
            {
                return Value - (Quantity * CostBasis);
            }
        }

        public string TermType 
        { 
            get 
            {
                var termType = Models.TermType.LongTerm;
                if (AcquiredDate > DateTime.UtcNow.AddYears(-1)) 
                {
                    termType = Models.TermType.ShortTerm;
                }

                return termType.AsString(EnumFormat.Description);
            } 
        }

        public double CurrentPrice
        {
            get
            {
                return Stock.Price;
            }
        }
        
    }
}
