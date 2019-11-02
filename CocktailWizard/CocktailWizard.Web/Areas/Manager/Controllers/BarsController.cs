using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public BarsController(IViewModelMapper<BarDto, BarViewModel> barViewModelMapper, BarService barService)
        {
            this.barViewModelMapper = barViewModelMapper;
            this.barService = barService;
        }

        // GET: Manager/Bars
        public async Task<IActionResult> Index()
        {
            var allBars = await this.barService.GetAllBarsAsync();
            var barVMs = this.barViewModelMapper.MapFrom(allBars);

            return View(barVMs);
        }

        // GET: Manager/Bars/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barDto = await this.barService.GetBarAsync(id);
            var barVM = this.barViewModelMapper.MapFrom(barDto);

            return View(barVM);
        }

        // GET: Manager/Bars/Create
        public IActionResult Create()
        {
            return View();
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

            ModelState.AddModelError(string.Empty, "//TODO");
            return View(barVM);

        }

        // GET: Manager/Bars/Edit/
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barDto = await this.barService.GetBarAsync(id);
            var barVM = this.barViewModelMapper.MapFrom(barDto);

            return View(barVM);
        }

        // POST: Manager/Bars/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BarViewModel barVM)
        {

            if (ModelState.IsValid)
            {
                var barDto = this.barViewModelMapper.MapFrom(barVM);

                await this.barService.EditAsync(barDto);

                return RedirectToAction("Details", new { id = barDto.Id});
            }

            ModelState.AddModelError(string.Empty, "TODO");

            return View(barVM);
        }

        // GET: Manager/Bars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var bar = await _context.Bars
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (bar == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // POST: Manager/Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var bar = await _context.Bars.FindAsync(id);
            //_context.Bars.Remove(bar);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

    }
}
