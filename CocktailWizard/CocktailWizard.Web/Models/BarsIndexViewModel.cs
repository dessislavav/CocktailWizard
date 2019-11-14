using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Models
{
    public class BarsIndexViewModel
    {
        public int? PrevPage { get; set; }

        public int CurrPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }
        public ICollection<BarViewModel> TenBars { get; set; }
        public BarViewModel CreateNewBar { get; set; }
    }
}
