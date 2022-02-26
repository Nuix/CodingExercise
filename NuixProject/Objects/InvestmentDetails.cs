using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nuix_Project.APIObjects
{
    public class InvestmentDetails : Investment
    {

        public decimal CostBasisPerShare { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal CurrentValue => NumberOfShares * CurrentPrice;

        public decimal GainLoss => CurrentValue - (CostBasisPerShare * NumberOfShares);

        public int NumberOfShares { get; set; }

        public DateTime PurchaseDateTime { get; set; }

        public string Term => (PurchaseDateTime.AddYears(1) >= DateTime.Now) ? "Short Term" : "Long Term";

    }
}
