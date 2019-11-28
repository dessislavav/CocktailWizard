using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.Providers;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System;
using System.IO;
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
        private readonly IFileServiceProvider fileServiceProvider;

        public CocktailsController(IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper, 
                                   ICocktailService cocktailService, 
                                   IToastNotification toastNotification,
                                   IFileServiceProvider fileServiceProvider)
        {
            this.cocktailViewModelMapper = cocktailViewModelMapper ?? throw new ArgumentNullException(nameof(cocktailViewModelMapper));
            this.cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this.toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
            this.fileServiceProvider = fileServiceProvider ?? throw new ArgumentNullException(nameof(fileServiceProvider));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCocktailViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CocktailViewModel.File == null)
                {
                    this.toastNotification.AddWarningToastMessage("Please upload a valid image");
                    return RedirectToAction("Index", "Cocktails", new { area = "" });
                }

                var cocktailUploadsFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\img\\cocktails");
                this.fileServiceProvider.CreateFolder(cocktailUploadsFolder);
                var newFileName = $"{Guid.NewGuid()}_{model.CocktailViewModel.File.FileName}";
                string fullFilePath = Path.Combine(cocktailUploadsFolder, newFileName);
                string cocktailDBImageLocationPath = $"/assets/img/cocktails/{newFileName}";
                model.CocktailViewModel.ImagePath = cocktailDBImageLocationPath;

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.CocktailViewModel.File.CopyToAsync(memoryStream);
                        var x = memoryStream.ToArray();
                        stream.Write(x, 0, x.Length);
                    }
                }

                var tempCocktail = this.cocktailViewModelMapper.MapFrom(model.CocktailViewModel);
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
        public async Task<IActionResult> Edit(Guid id, string newName, string newInfo)
        {
            try
            {
                //var cocktailDto = await this.cocktailService.EditAsync(id, newName, newInfo);
                //var cocktailVM = this.cocktailViewModelMapper.MapFrom(cocktailDto);
                //this.toastNotification.AddSuccessToastMessage("Cocktail successfully edited");
                //return View(cocktailVM);
                var cocktailDto = await this.cocktailService.EditAsync(id, newName, newInfo);
                this.toastNotification.AddSuccessToastMessage("Cocktail successfully edited");
                return RedirectToAction("Index", "Cocktails", new { area = "" });


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBars(AddBarsToCocktailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cocktaiDto = await this.cocktailService.GetCocktailAsync(model.Id);

                await this.cocktailService.AddBarsAsync(cocktaiDto, model.SelectedBars);

                this.toastNotification.AddSuccessToastMessage("Cocktails successfuly added");
                return RedirectToAction("Details", new { id = cocktaiDto.Id });
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            this.toastNotification.AddWarningToastMessage("Bars were not added");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBars(RemoveBarsFromCocktailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cocktailDto = await this.cocktailService.GetCocktailAsync(model.Id);

                await this.cocktailService.RemoveBarsAsync(cocktailDto, model.SelectedBars);

                this.toastNotification.AddSuccessToastMessage("Bars successfuly removed");
                return RedirectToAction("Details", new { id = cocktailDto.Id });
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            this.toastNotification.AddWarningToastMessage("Bars were not removed");
            return View(model);
        }
    }
}
