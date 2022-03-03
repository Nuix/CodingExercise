using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business.Models
{
    public class Listing
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public decimal CurrentPrice { get; set; }
    }
}
