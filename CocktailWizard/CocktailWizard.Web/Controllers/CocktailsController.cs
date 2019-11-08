using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;
        private readonly IViewModelMapper<DetailsCocktailDto, DetailsCocktailViewModel> detailsCocktailViewModelMapper;
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly CocktailService cocktailService;
        public CocktailsController(CocktailService cocktailService, IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper, IViewModelMapper<DetailsCocktailDto, DetailsCocktailViewModel> detailsCocktailViewModelMapper, IViewModelMapper<BarDto, BarViewModel> barViewModelMapper)
        {
            this.cocktailService = cocktailService;
            this.cocktailViewModelMapper = cocktailViewModelMapper;
            this.detailsCocktailViewModelMapper = detailsCocktailViewModelMapper;
            this.barViewModelMapper = barViewModelMapper;
        }
        // GET: /Cocktails
        public async Task<IActionResult> Index()
        {
            var dtoCocktails = await this.cocktailService.GetAllCocktailsAsync();
            var mappedCocktails = this.cocktailViewModelMapper.MapFrom(dtoCocktails);

            return View(mappedCocktails);
        }

        // GET: /Cocktails/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dtoCocktail = await this.cocktailService.GetCocktailsBars(id);
            var barsVM = this.barViewModelMapper.MapFrom(dtoCocktail.Bars);
            var cocktailVM = this.detailsCocktailViewModelMapper.MapFrom(dtoCocktail);
            cocktailVM.Bars = barsVM;
            return View(cocktailVM);
        }

    }
}