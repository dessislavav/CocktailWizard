using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.ViewComponents
{
    public class RemoveBarsFromCocktailViewComponent : ViewComponent
    {
        private readonly ICocktailService cocktailService;

        public RemoveBarsFromCocktailViewComponent(ICocktailService cocktailService)
        {
            this.cocktailService = cocktailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var model = new RemoveBarsFromCocktailsViewModel();
            model.Id = id;
            var cocktailDto = await cocktailService.GetCocktailBarsAsync(id);
            model.AllBars = cocktailDto.Bars.Select(c => new SelectListItem(c.Name, c.Name)).ToList();

            return View(model);
        }
    }
}
