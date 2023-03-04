using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPerformance.Models
{
	public class InvestmentDetails
	{
        [Key]
        public Guid OwnershipId { get; set; }

        public decimal CostBasis { get; set; }
        public decimal CurrentPrice { get; set; }
        public int NumberOfShares { get; set; }

        public DateTime PurchaseDateTime { get; set; }

        public decimal CurrentValue => NumberOfShares * CurrentPrice;
        public decimal GainLoss => CurrentValue - (CostBasis * NumberOfShares);

        public string Term => (PurchaseDateTime.AddYears(1) >= DateTime.Now) ? "Short Term" : "Long Term";

        public Guid InvestmentId { get; set; }
        public Guid UserId { get; set; }

    }
}

