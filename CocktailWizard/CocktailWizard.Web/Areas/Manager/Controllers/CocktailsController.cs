using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class CocktailsController : Controller
    {
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;
        private readonly ICocktailService cocktailService;
        private readonly IToastNotification toastNotification;

        public CocktailsController(IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper, 
                                   ICocktailService cocktailService, 
                                   IToastNotification toastNotification)
        {
            this.cocktailViewModelMapper = cocktailViewModelMapper ?? throw new ArgumentNullException(nameof(cocktailViewModelMapper));
            this.cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this.toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CocktailViewModel cocktailViewModel)
        {
            if (ModelState.IsValid)
            {
                var tempCocktail = this.cocktailViewModelMapper.MapFrom(cocktailViewModel);
                var cocktailDto = await this.cocktailService.CreateAsync(tempCocktail);

                this.toastNotification.AddSuccessToastMessage("Cocktail successfully created");
                return RedirectToAction("Details", new { id = cocktailDto.Id });
            }

            this.toastNotification.AddWarningToastMessage("Incorrect input, please try again");
            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            return RedirectToAction("Index", "Cocktails", new { area = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string newName, string newInfo, string newImagePath)
        {
            try
            {
                var cocktailDto = await this.cocktailService.EditAsync(id, newName, newInfo, newImagePath);
                var cocktailVM = this.cocktailViewModelMapper.MapFrom(cocktailDto);
                this.toastNotification.AddSuccessToastMessage("Cocktail successfully edited");
                return View(cocktailVM);
            }
            catch (Exception)
            {
                this.toastNotification.AddWarningToastMessage("Incorrect input, please try again");
                return RedirectToAction("Index", "Cocktails", new { area = "" });
            }

        }

        // POST: Manager/Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await this.cocktailService.DeleteAsync(id);
                this.toastNotification.AddSuccessToastMessage("Cocktail successfully deleted");
                return RedirectToAction("Index", "Cocktails", new { area = "" });
            }
            catch (Exception)
            {
                this.toastNotification.AddWarningToastMessage("Cocktail couldn't be deleted");
                return RedirectToAction("Index", "Cocktails", new { area = "" });
            }
        }
    }
}
