using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;
        private readonly IViewModelMapper<DetailsCocktailDto, DetailsCocktailViewModel> detailsCocktailViewModelMapper;
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly ICocktailService cocktailService;
        private readonly IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper;
        private readonly ICocktailCommentService cocktailCommentService;
        private readonly ICocktailRatingService cocktailRatingService;
        private readonly IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper;

        public CocktailsController(ICocktailService cocktailService, 
            IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper, 
            IViewModelMapper<DetailsCocktailDto, DetailsCocktailViewModel> detailsCocktailViewModelMapper, 
            IViewModelMapper<BarDto, BarViewModel> barViewModelMapper,
            IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper,
            ICocktailCommentService cocktailCommentService,
            IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper,
            ICocktailRatingService cocktailRatingService)
        {
            this.cocktailService = cocktailService;
            this.cocktailViewModelMapper = cocktailViewModelMapper;
            this.detailsCocktailViewModelMapper = detailsCocktailViewModelMapper;
            this.barViewModelMapper = barViewModelMapper;
            this.cocktailCommentVmMapper = cocktailCommentVmMapper;
            this.cocktailCommentService = cocktailCommentService;
            this.cocktailRatingService = cocktailRatingService;
            this.cocktailRatingVmMapper = cocktailRatingVmMapper;
        }
        // GET: /Cocktails
        public async Task<IActionResult> Index(int? currPage)
        {
            var currentPage = currPage ?? 1;

            var totalPages = await this.cocktailService.GetPageCountAsync(10);
            var tenCocktails = await this.cocktailService.GetTenCocktailsOrderedByNameAsync(currentPage);
            var mappedTenCocktails = this.cocktailViewModelMapper.MapFrom(tenCocktails);

            var model = new CocktailsIndexViewModel()
            {
                CurrPage = currentPage,
                TotalPages = totalPages,
                TenCocktails = mappedTenCocktails,
            };

            if (totalPages > currentPage)
            {
                model.NextPage = currentPage + 1;
            }

            if (currentPage > 1)
            {
                model.PrevPage = currentPage - 1;
            }

            return View(model);
        }

        // GET: /Cocktails/Details/
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dtoCocktail = await this.cocktailService.GetCocktailsBars(id);

            var cocktailCommentDtos = await this.cocktailCommentService.GetCocktailCommentsAsync(id);
            var cocktailCommentVM = this.cocktailCommentVmMapper.MapFrom(cocktailCommentDtos);

            var cocktailRatingDtos = await this.cocktailRatingService.GetAllRatingsAsync(id);
            var cocktailRatingVM = this.cocktailRatingVmMapper.MapFrom(cocktailRatingDtos);

            var barsVM = this.barViewModelMapper.MapFrom(dtoCocktail.Bars);
            var cocktailVM = this.detailsCocktailViewModelMapper.MapFrom(dtoCocktail);
            cocktailVM.Bars = barsVM;

            //TODO: Fix logic here
            if (cocktailVM != null)
            {
                cocktailVM.CocktailCommentViewModels = cocktailCommentVM;
            }
            else
            {
                cocktailVM.CocktailCommentViewModels = new List<CocktailCommentViewModel>();
            }

            if (cocktailVM != null)
            {
                cocktailVM.CocktailRatingViewModels = cocktailRatingVM;
            }
            else
            {
                cocktailVM.CocktailRatingViewModels = new List<CocktailRatingViewModel>();
            }

            return View(cocktailVM);
        }

    }
}