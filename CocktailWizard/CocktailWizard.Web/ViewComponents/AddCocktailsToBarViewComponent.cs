using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.ViewComponents
{
    public class AddCocktailsToBarViewComponent : ViewComponent
    {

        private readonly ICocktailService cocktailService;

        public AddCocktailsToBarViewComponent(ICocktailService cocktailService)
        {
            this.cocktailService = cocktailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var model = new AddCocktailsToBarsViewModel();
            var allCocktailsDto = await this.cocktailService.GetAllCocktailsAsync();
            model.Id = id;
            model.AllCocktails = allCocktailsDto
                .Select(c => new SelectListItem(c.Name, c.Name))
                .ToList();

            return View(model);
        }
    }
}
