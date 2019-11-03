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

        // GET: Manager/Cocktails
        public async Task<IActionResult> Index()
        {
            var allCocktails = await this.cocktailService.GetAllCocktailsAsync();
            var allCocktailsVMs = this.cocktailViewModelMapper.MapFrom(allCocktails);

            return View(allCocktailsVMs);
        }

        // GET: Manager/Cocktails/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktailDto = await this.cocktailService.GetCocktailAsync(id);
            var cocktailVM = this.cocktailViewModelMapper.MapFrom(cocktailDto);

            return View(cocktailVM);
        }

        // GET: Manager/Cocktails/Create
        public async Task<IActionResult> Create(CreateCocktailViewModel createCocktailVM)
        {
                var allIngredients = await this.ingredientService.GetAllIngredientsAsync();

                createCocktailVM.AllAvailableIngredients = allIngredients
                    .Select(b => new SelectListItem(b.Name, b.Name))
                    .ToList();

                return View(createCocktailVM);
        }

        // POST: Manager/Cocktails/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(CocktailViewModel cocktailViewModel)
        {
            if (ModelState.IsValid)
            {
                var tempCocktail = this.cocktailViewModelMapper.MapFrom(cocktailViewModel);
                var cocktailDto = await this.cocktailService.CreateAsync(tempCocktail);

                return RedirectToAction("Details", new { id = cocktailDto.Id });
            }

            ModelState.AddModelError(string.Empty, "//TODO");
            return View(cocktailViewModel);
        }

        // GET: Manager/Cocktails/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktailDto = await this.cocktailService.GetCocktailAsync(id);
            var cocktailVM = this.cocktailViewModelMapper.MapFrom(cocktailDto);

            return View(cocktailVM);
        }

        // POST: Manager/Cocktails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CocktailViewModel cocktailVM)
        {
            if (ModelState.IsValid)
            {
                var cocktailDto = this.cocktailViewModelMapper.MapFrom(cocktailVM);

                await this.cocktailService.EditAsync(cocktailDto);

                return RedirectToAction("Details", new { id = cocktailDto.Id });
            }

            ModelState.AddModelError(string.Empty, "TODO");

            return View(cocktailVM);
        }

        // GET: Manager/Cocktails/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktailDto = await this.cocktailService.GetCocktailAsync(id);

            if (cocktailDto == null)
            {
                return NotFound();
            }
            var cocktailViewModel = this.cocktailViewModelMapper.MapFrom(cocktailDto);

            return View(cocktailViewModel);
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

            return RedirectToAction(nameof(Index));
        }
    }
}
