using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Models
{
    public class AddCocktailsToBarsViewModel
    {
        public Guid Id { get; set; } // BarViewModelId
        public List<SelectListItem> AllCocktails { get; set; }
        public List<string> SelectedCocktails { get; set; }
    }
}
