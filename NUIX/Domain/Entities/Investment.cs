using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Investment
    {
        public int InvestmentId { get; set; }
        public int InvestmentType { get; set; }
        public int UserId { get; set; }
        public string InvestmentName { get; set; }

    }
}
