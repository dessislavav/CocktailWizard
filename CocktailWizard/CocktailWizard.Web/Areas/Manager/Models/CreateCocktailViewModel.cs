using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Models
{
    public class CreateCocktailViewModel
    {
        public CocktailViewModel CocktailViewModel {get; set; }
        public List<SelectListItem> AllAvailableIngredients { get; set; }
        public IFormFile File { get; set; }
    }
}
