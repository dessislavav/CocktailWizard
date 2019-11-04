using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailWizard.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class IngredientsController : Controller
    {
        private readonly IViewModelMapper<IngredientDto, IngredientViewModel> ingredientViewModelMapper;
        private readonly IngredientService ingredientService;

        public IngredientsController(IViewModelMapper<IngredientDto, IngredientViewModel> ingredientViewModelMapper, IngredientService ingredientService)
        {
            this.ingredientViewModelMapper = ingredientViewModelMapper ?? throw new ArgumentException(nameof(ingredientViewModelMapper));
            this.ingredientService = ingredientService ?? throw new ArgumentException(nameof(ingredientService));
        }

        public async Task<IActionResult> Index()
        {
            var allIngredients = await this.ingredientService.GetAllIngredientsAsync();
            var allIngredientsVM = this.ingredientViewModelMapper.MapFrom(allIngredients);

            return View(allIngredientsVM);
        }

        // POST: Manager/Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.ingredientService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]IngredientViewModel ingredientViewModel)
        {
            var ingredientDto = this.ingredientViewModelMapper.MapFrom(ingredientViewModel);
                
            await this.ingredientService.CreateIngredientAsync(ingredientDto);

            return Json(ingredientViewModel);
        }

        // POST: Manager/Ingredients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IngredientViewModel ingredientViewModel)
        {
            if (ModelState.IsValid)
            {

            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);

            return RedirectToAction(nameof(Index));
        }
    }
}