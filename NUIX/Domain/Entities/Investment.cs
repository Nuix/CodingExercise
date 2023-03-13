using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Investment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvestmentId { get; set; }
        public int InvestmentTypeId { get; set; }
        public int UserId { get; set; }
        public string InvestmentName { get; set; }

    }
}
