using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingExercise.Models
{
    [PrimaryKey(nameof(Id))]
    public class User
    {
        // Assumption that this class will contain more information in the future (name, tax identifier, and so on)
        public User() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        public List<Investment> Investments { get; set; } = new List<Investment>();
    }
}
