using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    public class BarsController : Controller
    {
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly BarService barService;

        public BarsController(IViewModelMapper<BarDto, BarViewModel> barViewModelMapper, BarService barService)
        {
            this.barViewModelMapper = barViewModelMapper;
            this.barService = barService;
        }
        public async Task<IActionResult> Index()
        {
            var allBars = await this.barService.GetAllBarsAsync();
            var barVMs = this.barViewModelMapper.MapFrom(allBars);

            return View(barVMs);
        }

        // GET: /Bars/Details
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
    }
}