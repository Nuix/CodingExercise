using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Data.Model
{
    public partial class Listing
    {
        public Listing()
        {
            UserInvestments = new HashSet<UserInvestment>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CompanyName { get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrentPrice { get; set; }

        [InverseProperty(nameof(UserInvestment.Listing))]
        public virtual ICollection<UserInvestment> UserInvestments { get; set; }
    }
}
