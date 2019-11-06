using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    public class BarsController : Controller
    {
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly IViewModelMapper<SearchBarDto, BarViewModel> searchBarVmMapper;
        private readonly BarService barService;

        public BarsController(
            IViewModelMapper<BarDto, BarViewModel> barViewModelMapper,
            IViewModelMapper<SearchBarDto, BarViewModel> searchBarVmMapper,
            BarService barService)
        {
            this.barViewModelMapper = barViewModelMapper;
            this.barService = barService;
            this.searchBarVmMapper = searchBarVmMapper;
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

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]SearchBarViewModel barSearchVM)
        {
            if (string.IsNullOrWhiteSpace(barSearchVM.SearchName) || barSearchVM.SearchName.Length < 3)
            {
                return View();
            }

            var result = await this.barService.Search(barSearchVM.SearchName, barSearchVM.SearchByName, barSearchVM.SearchByAddress, barSearchVM.SearchByRating);
            barSearchVM.SearchResults = result.Select(b => this.searchBarVmMapper.MapFrom(b)).ToList();

            return View(barSearchVM);

        }
    }
}