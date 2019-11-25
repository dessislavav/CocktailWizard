using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.ViewComponents
{
    public class RemoveCocktailsFromBarViewComponent : ViewComponent
    {
        private readonly IBarService barService;

        public RemoveCocktailsFromBarViewComponent(IBarService barService)
        {
            this.barService = barService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var model = new RemoveCocktailsFromBarsViewModel();
            model.Id = id;
            var barDto = await this.barService.GetBarCocktailsAsync(id);
            model.AllCocktails = barDto.Cocktails.Select(c => new SelectListItem(c.Name, c.Name)).ToList();

            return View(model);
        }
    }
}
