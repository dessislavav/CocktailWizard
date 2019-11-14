using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CocktailWizard.Services.ConstantMessages;

namespace CocktailWizard.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class CocktailsController : Controller
    {
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;
        private readonly CocktailService cocktailService;
        private readonly IngredientService ingredientService;

        public CocktailsController(IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper, CocktailService cocktailService, IngredientService ingredientService)
        {
            this.cocktailViewModelMapper = cocktailViewModelMapper;
            this.cocktailService = cocktailService;
            this.ingredientService = ingredientService;
        }

        public async Task<IActionResult> Create(CreateCocktailViewModel createCocktailVM)
        {
            var allIngredients = await this.ingredientService.GetIngredientsAsync();

            createCocktailVM.AllAvailableIngredients = allIngredients
                .Select(b => new SelectListItem(b.Name, b.Name))
                .ToList();

            return View(createCocktailVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CocktailViewModel cocktailViewModel)
        {
            if (ModelState.IsValid)
            {
                var tempCocktail = this.cocktailViewModelMapper.MapFrom(cocktailViewModel);
                var cocktailDto = await this.cocktailService.CreateAsync(tempCocktail);

                return RedirectToAction("Details", new { id = cocktailDto.Id });
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            return View(cocktailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string newName, string newInfo, string newImagePath)
        {
            var cocktailDto = await this.cocktailService.EditAsync(id, newName, newInfo, newImagePath);

            var cocktailVM = this.cocktailViewModelMapper.MapFrom(cocktailDto);

            return View(cocktailVM);
        }

        // POST: Manager/Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.cocktailService.DeleteAsync(id);

            return RedirectToAction("Index", "Cocktails", new { area = "" });
        }
    }
}
