using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NUIX.InvestmentPerformance.API.Models.Enums
{
    public enum InvestmentTerm
    {
        [Display(Name = "Short Term")]
        ShortTerm = 0,

        [Display(Name = "Long Term")]
        LongTerm = 1,
    }
}
