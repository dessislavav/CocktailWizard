using CocktailWizard.Services;
using CocktailWizard.Web.Areas.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.ViewComponents
{
    public class AddBarsToCocktailViewComponent : ViewComponent
    {
        private readonly IBarService barService;

        public AddBarsToCocktailViewComponent(IBarService barService)
        {
            this.barService = barService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var model = new AddBarsToCocktailsViewModel();
            var allBarsDto = await barService.GetAllBarsAsync();
            model.Id = id;
            model.AllBars = allBarsDto
                .Select(c => new SelectListItem(c.Name, c.Name))
                .ToList();

            return View(model);
        }
    }
}
