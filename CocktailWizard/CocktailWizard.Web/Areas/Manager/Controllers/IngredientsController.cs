using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Web.Areas.Manager.Models;
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

        public IngredientsController(IViewModelMapper<IngredientDto, IngredientViewModel> ingredientViewModelMapper,
                                     IngredientService ingredientService)
        {
            this.ingredientViewModelMapper = ingredientViewModelMapper ?? throw new ArgumentException(nameof(ingredientViewModelMapper));
            this.ingredientService = ingredientService ?? throw new ArgumentException(nameof(ingredientService));
        }

        public async Task<IActionResult> Index(int? currPage)
        {
            var currentPage = currPage ?? 1;

            var totalPages = await this.ingredientService.GetPageCountAsync(10);
            var tenIngredients = await this.ingredientService.GetTenIngredientsOrderedByNameAsync(currentPage);
            var mappedTenIngredients = this.ingredientViewModelMapper.MapFrom(tenIngredients);

            var model = new IngredientsListViewModel()
            {
                CurrPage = currentPage,
                TotalPages = totalPages,
                TenIngredients = mappedTenIngredients,
            };

            if (totalPages > currentPage)
            {
                model.NextPage = currentPage + 1;
            }

            if (currentPage > 1)
            {
                model.PrevPage = currentPage - 1;
            }

            return View(model);
        }

        // POST: Manager/Ingredients/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
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
        public async Task<IActionResult> Edit(Guid id, string newName)
        {
            if (ModelState.IsValid)
            {
                var ingredientDto = await this.ingredientService.GetIngredientAsync(id);
                await this.ingredientService.EditAsync(ingredientDto);
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);

            return RedirectToAction(nameof(Index));
        }
    }
}