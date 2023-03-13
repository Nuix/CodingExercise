using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Models
{
    [PrimaryKey(nameof(Id))]
    public class Stock
    {
        public Stock(string name, double price)
        {
            Name = name;
            Price = price;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
