using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Data.Model
{
    public partial class UserInvestment
    {
        [Key]
        public int Id { get; set; }
        public int ListingId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountOfShares { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SharePurchasePrice { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PurchaseDate { get; set; }

        [ForeignKey(nameof(ListingId))]
        [InverseProperty("UserInvestments")]
        public virtual Listing Listing { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserInvestments")]
        public virtual User User { get; set; } = null!;
    }
}
