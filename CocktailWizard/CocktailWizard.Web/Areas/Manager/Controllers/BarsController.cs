using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
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
    public class BarsController : Controller
    {
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly IBarService barService;
        private readonly IToastNotification toastNotification;

        public BarsController(IViewModelMapper<BarDto, BarViewModel> barViewModelMapper, IBarService barService, IToastNotification toastNotification)
        {
            this.barViewModelMapper = barViewModelMapper ?? throw new ArgumentNullException(nameof(barViewModelMapper));
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
        }

        // POST: Manager/Bars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarViewModel barVM)
        {
            if (ModelState.IsValid)
            {
                var tempBar = this.barViewModelMapper.MapFrom(barVM);
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
                return RedirectToAction("Details", new { id = barDto.Id});
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
        public async Task<IActionResult> AddCocktails(AddCocktailsToBarsViewModel addCocktailsVM)
        {
            if (ModelState.IsValid)
            {
                var barDto = await this.barService.GetBarAsync(addCocktailsVM.Id);

                await this.barService.AddCocktailsAsync(barDto, addCocktailsVM.SelectedCocktails);

                this.toastNotification.AddSuccessToastMessage("Cocktails successfuly added");
                return RedirectToAction("Details", new { id = barDto.Id });
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            this.toastNotification.AddWarningToastMessage("Cocktails were not added");
            return View(addCocktailsVM);
        }

    }
}
