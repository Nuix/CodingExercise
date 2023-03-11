using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Dtos
{
    public class StockInvestmentDetail
    {
        public int InvestmentId { get; set; }
        public int StockId { get; set; }
        public DateTime PurchasedDate { get; set; }// this is stored in UTC time
        public decimal CostBasisPerShare { get; set; }
        public int NumberOfShares { get; set; }
        public decimal CurrentSharePrice { get; set; }

        public StockPeriod Term { get {

                int daysDiff = DateTime.UtcNow.Subtract(PurchasedDate).Days;

                return daysDiff <= 365 ? StockPeriod.ShortTerm : StockPeriod.LongTerm;
            } 
        }

        public decimal CurrentValue => NumberOfShares * CurrentSharePrice;
        public decimal PurshasedValue => NumberOfShares * CostBasisPerShare;
        public decimal TotalGainLoss => CurrentValue - PurshasedValue;
    }
}
