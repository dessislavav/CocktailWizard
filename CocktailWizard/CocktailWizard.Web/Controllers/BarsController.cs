using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    public class BarsController : Controller
    {
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly IViewModelMapper<SearchBarDto, BarViewModel> searchBarVmMapper;
        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper;
        private readonly BarCommentService barCommentService;
        private readonly IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper;
        private readonly BarRatingService barRatingService;
        private readonly BarService barService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;

        public BarsController(
            IViewModelMapper<BarDto, BarViewModel> barViewModelMapper,
            IViewModelMapper<SearchBarDto, BarViewModel> searchBarVmMapper,
            IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper,
            BarCommentService barCommentService,
            IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper,
            BarRatingService barRatingService,
            BarService barService,
            IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper)
        {
            this.barViewModelMapper = barViewModelMapper;
            this.barService = barService;
            this.cocktailViewModelMapper = cocktailViewModelMapper;
            this.searchBarVmMapper = searchBarVmMapper;
            this.barCommentVmMapper = barCommentVmMapper;
            this.barCommentService = barCommentService;
            this.barRatingVmMapper = barRatingVmMapper;
            this.barRatingService = barRatingService;
        }

        public async Task<IActionResult> Index(int? currPage)
        {
            var currentPage = currPage ?? 1;

            var totalPages = await this.barService.GetPageCountAsync(10);
            var tenBars = await this.barService.GetTenBarsOrderedByNameAsync(currentPage);
            var mappedTenBars = this.barViewModelMapper.MapFrom(tenBars);

            var model = new BarsIndexViewModel()
            {
                CurrPage = currentPage,
                TotalPages = totalPages,
                TenBars = mappedTenBars,
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

        // GET: /Bars/Details
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barDto = await this.barService.GetBarCocktails(id);

            var barCommentDtos = await this.barCommentService.GetBarCommentsAsync(id);
            var barCommentVM = this.barCommentVmMapper.MapFrom(barCommentDtos);

            var barRatingDtos = await this.barRatingService.GetAllRatingsAsync(id);
            var barRatingVM = this.barRatingVmMapper.MapFrom(barRatingDtos);

            var cocktailsVM = this.cocktailViewModelMapper.MapFrom(barDto.Cocktails);
            var barVM = this.barViewModelMapper.MapFrom(barDto);
            barVM.Cocktails = cocktailsVM;

            //TODO: Fix logic here
            if (barCommentVM != null)
            {
                barVM.BarCommentViewModels = barCommentVM;
            }
            else
            {
                barVM.BarCommentViewModels = new List<BarCommentViewModel>();
            }
            if (barCommentVM != null)
            {
                barVM.BarRatingViewModels = barRatingVM;
            }
            else
            {
                barVM.BarRatingViewModels = new List<BarRatingViewModel>();
            }


            return View(barVM);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]SearchBarViewModel barSearchVM)
        {
            if (string.IsNullOrWhiteSpace(barSearchVM.SearchName) || barSearchVM.SearchName.Length < 3)
            {
                return View();
            }

            var result = await this.barService.SearchAsync(barSearchVM.SearchName, barSearchVM.SearchByName, barSearchVM.SearchByAddress, barSearchVM.SearchByRating);
            barSearchVM.SearchResults = result.Select(b => this.searchBarVmMapper.MapFrom(b)).ToList();

            return View(barSearchVM);

        }
    }
}