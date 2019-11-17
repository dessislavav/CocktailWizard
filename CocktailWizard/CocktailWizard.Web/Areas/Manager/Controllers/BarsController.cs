using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly BarService barService;
        private readonly CocktailService cocktailService;

        public BarsController(IViewModelMapper<BarDto, BarViewModel> barViewModelMapper, BarService barService, CocktailService cocktailService)
        {
            this.barViewModelMapper = barViewModelMapper;
            this.barService = barService;
            this.cocktailService = cocktailService;
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

                return RedirectToAction("Details", new { id = barDto.Id });

            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            return View(barVM);

        }

        // POST: Manager/Bars/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string newName, string newInfo, string newAddress, string newPhone, string newImagePath)
        {
            if (ModelState.IsValid)
            {
                var barDto = await this.barService.EditAsync(id, newName, newInfo, newAddress, newPhone, newImagePath);

                return RedirectToAction("Details", new { id = barDto.Id});
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            return View();
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
                throw;
            }
            
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

                return RedirectToAction("Details", new { id = barDto.Id });
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            return View(addCocktailsVM);
        }

    }
}
