using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CocktailWizard.Models;
using CocktailWizard.Services;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using CocktailWizard.Data.DtoEntities;
using Microsoft.Extensions.Caching.Memory;

namespace CocktailWizard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly BarService barService;
        private readonly CocktailService cocktailService;
        private readonly IMemoryCache cache;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;


        public HomeController(
            IViewModelMapper<BarDto, BarViewModel> barViewModelMapper, 
            IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper, 
            BarService barService, 
            CocktailService cocktailService, 
            IMemoryCache cache)
        {
            this.barViewModelMapper = barViewModelMapper; ;
            this.cocktailViewModelMapper = cocktailViewModelMapper;
            this.barService = barService;
            this.cocktailService = cocktailService;
            this.cache = cache;
        }

        public async Task<IActionResult> Index(HomeViewModel homeVM)
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
                return await this.barService.GetTopBars(3);
            });

            return topBarsDtos;
        }

        [NonAction]
        private async Task<ICollection<CocktailDto>> CacheCocktailsDtos()
        {
            var topCocktailsDtos = await cache.GetOrCreateAsync<ICollection<CocktailDto>>("Cocktails", async (cacheEntry) =>
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromDays(1);
                return await this.cocktailService.GetTopCocktails(3);
            });

            return topCocktailsDtos;
        }
    }
}
