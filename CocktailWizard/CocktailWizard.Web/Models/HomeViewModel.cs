using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Models
{
    public class HomeViewModel
    {
        public ICollection<BarViewModel> TopBars { get; set; }
        public ICollection<CocktailViewModel> TopCocktails { get; set; }
    }
}
