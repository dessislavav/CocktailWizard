using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class IngredientsController : Controller
    {
        private readonly IViewModelMapper<IngredientDto, IngredientViewModel> ingredientViewModelMapper;
        private readonly IIngredientService ingredientService;
        private readonly IToastNotification toastNotification;

        public IngredientsController(IViewModelMapper<IngredientDto, IngredientViewModel> ingredientViewModelMapper,
                                     IIngredientService ingredientService,
                                     IToastNotification toastNotification)
        {
            this.ingredientViewModelMapper = ingredientViewModelMapper ?? throw new ArgumentException(nameof(ingredientViewModelMapper));
            this.ingredientService = ingredientService ?? throw new ArgumentException(nameof(ingredientService));
            this.toastNotification = toastNotification ?? throw new ArgumentException(nameof(toastNotification));
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
            try
            {
                this.toastNotification.AddSuccessToastMessage("Ingredient successfully deleted");
                await this.ingredientService.DeleteAsync(id);
            }
            catch (Exception)
            {
                this.toastNotification.AddWarningToastMessage("Ingredient couldn't be deleted");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]IngredientViewModel ingredientViewModel)
        {
            var ingredientDto = this.ingredientViewModelMapper.MapFrom(ingredientViewModel);

            await this.ingredientService.CreateIngredientAsync(ingredientDto);
            this.toastNotification.AddSuccessToastMessage("Ingredient successfully created");
            return Json(ingredientViewModel);
        }


        // POST: Manager/Ingredients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string newName)
        {
            if (ModelState.IsValid)
            {
                await this.ingredientService.EditAsync(id, newName);
                this.toastNotification.AddSuccessToastMessage("Ingredient successfully edited");
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            this.toastNotification.AddSuccessToastMessage("Ingredient couldn't be edited");
            return RedirectToAction(nameof(Index));
        }
    }
}