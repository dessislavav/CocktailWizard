using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Models
{
    public class RemoveBarsFromCocktailsViewModel
    {
        public Guid Id { get; set; } // CocktailViewModelId
        public List<SelectListItem> AllBars { get; set; }
        public List<string> SelectedBars { get; set; }
    }
}
