using CocktailWizard.Web.Areas.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Models
{
    public class CocktailsIndexViewModel
    {
        public int? PrevPage { get; set; }

        public int CurrPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }
        public ICollection<CocktailViewModel> TenCocktails { get; set; }
        public CreateCocktailViewModel CreateNewCocktail { get; set; }
    }
}
