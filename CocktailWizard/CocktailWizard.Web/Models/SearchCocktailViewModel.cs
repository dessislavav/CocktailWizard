using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Models
{
    public class SearchCocktailViewModel
    {
        public string SearchName { get; set; }
        public bool SearchByName { get; set; }
        public bool SearchByRating { get; set; }
        public double Value { get; set; }

        public IReadOnlyCollection<CocktailViewModel> SearchResults { get; set; } = new List<CocktailViewModel>();
    }
}
