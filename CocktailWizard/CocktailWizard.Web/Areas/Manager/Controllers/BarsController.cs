using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
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
    public class BarsController : Controller
    {
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly IBarService barService;
        private readonly IToastNotification toastNotification;
        private readonly IFileServiceProvider fileServiceProvider;

        public BarsController(IViewModelMapper<BarDto, BarViewModel> barViewModelMapper,
                              IBarService barService,
                              IToastNotification toastNotification,
                              IFileServiceProvider fileServiceProvider)
        {
            this.barViewModelMapper = barViewModelMapper ?? throw new ArgumentNullException(nameof(barViewModelMapper));
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
            this.fileServiceProvider = fileServiceProvider ?? throw new ArgumentNullException(nameof(fileServiceProvider));
        }

        // POST: Manager/Bars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File == null)
                {
                    this.toastNotification.AddWarningToastMessage("Please upload a valid image");
                    return RedirectToAction("Index", "Bars", new { area = "" });
                }

                var barUploadsFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\img\\bars");
                this.fileServiceProvider.CreateFolder(barUploadsFolder);
                var newFileName = $"{Guid.NewGuid()}_{model.File.FileName}";
                string fullFilePath = Path.Combine(barUploadsFolder, newFileName);
                string barDBImageLocationPath = $"/assets/img/bars/{newFileName}";

                model.ImagePath = barDBImageLocationPath;

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.File.CopyToAsync(memoryStream);
                        var x = memoryStream.ToArray();
                        stream.Write(x, 0, x.Length);
                    }
                }

                var tempBar = this.barViewModelMapper.MapFrom(model);
                var barDto = await this.barService.CreateAsync(tempBar);

                this.toastNotification.AddSuccessToastMessage("Bar successfully created");
                return RedirectToAction("Details", new { id = barDto.Id });
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);

            this.toastNotification.AddWarningToastMessage("Incorrect input, please try again");
            return RedirectToAction("Index", "Bars", new { area = "" });
        }

        // POST: Manager/Bars/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string newName, string newInfo, string newAddress, string newPhone, string newImagePath)
        {
            if (ModelState.IsValid)
            {
                var barDto = await this.barService.EditAsync(id, newName, newInfo, newAddress, newPhone, newImagePath);

                this.toastNotification.AddSuccessToastMessage("Bar successfully edited");
                return RedirectToAction("Details", new { id = barDto.Id });
            }

            this.toastNotification.AddWarningToastMessage("Incorrect input, please try again");
            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            return RedirectToAction("Index", "Bars", new { area = "" });
        }

        // POST: Manager/Bars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {

            try
            {
                await this.barService.DeleteAsync(id);
            }
            catch (Exception)
            {
                this.toastNotification.AddWarningToastMessage("Bar couldn't be deleted");
            }

            this.toastNotification.AddSuccessToastMessage("Bar successfully deleted");
            return RedirectToAction("Index", "Bars", new { area = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCocktails(AddCocktailsToBarsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var barDto = await this.barService.GetBarAsync(model.Id);

                await this.barService.AddCocktailsAsync(barDto, model.SelectedCocktails);

                this.toastNotification.AddSuccessToastMessage("Cocktails successfuly added");
                return RedirectToAction("Details", new { id = barDto.Id });
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            this.toastNotification.AddWarningToastMessage("Cocktails were not added");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCocktails(RemoveCocktailsFromBarsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var barDto = await this.barService.GetBarAsync(model.Id);

                await this.barService.RemoveCocktailsAsync(barDto, model.SelectedCocktails);

                this.toastNotification.AddSuccessToastMessage("Cocktails successfuly removed");
                return RedirectToAction("Details", new { id = barDto.Id });
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            this.toastNotification.AddWarningToastMessage("Cocktails were not removed");
            return View(model);
        }

    }
}
