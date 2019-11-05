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

namespace CocktailWizard.Controllers
{
    public class HomeController : Controller
    {
        private IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private BarService barService;
        private CocktailService cocktailService;

        public IViewModelMapper<CocktailDto, CocktailViewModel> CocktailViewModelMapper { get; }

        public HomeController(IViewModelMapper<BarDto, BarViewModel> barViewModelMapper, IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper, BarService barService, CocktailService cocktailService)
        {
            this.barViewModelMapper = barViewModelMapper;
            CocktailViewModelMapper = cocktailViewModelMapper;
            this.barService = barService;
            this.cocktailService = cocktailService;
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index(HomeViewModel homeVM)
        {
            var topBarsDtos = await this.barService.GetTopBars(3);
            var topBarsVM = this.barViewModelMapper.MapFrom(topBarsDtos);

            var topCocktailsDtos = await this.cocktailService.GetTopCocktails(3);
            var topCocktailsVM = this.CocktailViewModelMapper.MapFrom(topCocktailsDtos);

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
    }
}
