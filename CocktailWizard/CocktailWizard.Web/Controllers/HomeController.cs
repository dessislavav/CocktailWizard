using CocktailWizard.Models;
using CocktailWizard.Services;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly ICocktailService cocktailService;
        private readonly IBarService barService;
        private readonly IMemoryCache cache;


        public HomeController(IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper,
                              IViewModelMapper<BarDto, BarViewModel> barViewModelMapper,
                              ICocktailService cocktailService,
                              IBarService barService,
                              IMemoryCache cache)
        {
            this.cocktailViewModelMapper = cocktailViewModelMapper ?? throw new ArgumentNullException(nameof(cocktailViewModelMapper));
            this.barViewModelMapper = barViewModelMapper ?? throw new ArgumentNullException(nameof(barViewModelMapper));
            this.cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<IActionResult> Index()
        {
            var topBarsVM = (await CacheBarsDtos())
                .Select(x => this.barViewModelMapper.MapFrom(x))
                .ToList();

            var topCocktailsVM = (await CacheCocktailsDtos())
                .Select(x => this.cocktailViewModelMapper.MapFrom(x))
                .ToList();

            var homeViewModel = new HomeViewModel
            {
                TopBars = topBarsVM,
                TopCocktails = topCocktailsVM
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        private async Task<ICollection<BarDto>> CacheBarsDtos()
        {
            var topBarsDtos = await cache.GetOrCreateAsync<ICollection<BarDto>>("Bars", async (cacheEntry) =>
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromDays(1);
                return await this.barService.GetTopBarsAsync(3);
            });

            return topBarsDtos;
        }

        [NonAction]
        private async Task<ICollection<CocktailDto>> CacheCocktailsDtos()
        {
            var topCocktailsDtos = await cache.GetOrCreateAsync<ICollection<CocktailDto>>("Cocktails", async (cacheEntry) =>
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromDays(1);
                return await this.cocktailService.GetTopCocktailsAsync(3);
            });

            return topCocktailsDtos;
        }
    }
}
