using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class InvestmentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvestmentTypeId { get; set; }
        public string InvestmentName { get;set; }
    }
}
