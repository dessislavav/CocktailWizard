using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    public class BarsController : Controller
    {
        private readonly IViewModelMapper<BarDto, BarViewModel> barViewModelMapper;
        private readonly IViewModelMapper<SearchBarDto, BarViewModel> searchBarVmMapper;
        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper;
        private readonly IBarCommentService barCommentService;
        private readonly IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper;
        private readonly IBarRatingService barRatingService;
        private readonly IBarService barService;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;
        private readonly UserManager<User> userManager;

        public BarsController(IViewModelMapper<BarDto, BarViewModel> barViewModelMapper,
                              IViewModelMapper<SearchBarDto, BarViewModel> searchBarVmMapper,
                              IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper,
                              IBarCommentService barCommentService,
                              IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper,
                              IBarRatingService barRatingService,
                              IBarService barService,
                              IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper, 
                              UserManager<User> userManager)
        {
            this.barViewModelMapper = barViewModelMapper;
            this.barService = barService;
            this.cocktailViewModelMapper = cocktailViewModelMapper;
            this.userManager = userManager;
            this.searchBarVmMapper = searchBarVmMapper;
            this.barCommentVmMapper = barCommentVmMapper;
            this.barCommentService = barCommentService;
            this.barRatingVmMapper = barRatingVmMapper;
            this.barRatingService = barRatingService;
        }

        public async Task<IActionResult> Index(int? currPage, string sortOrder)
        {
            this.ViewData["NameSortParm"] = (string.IsNullOrEmpty(sortOrder) || sortOrder == "Name") ? "name_desc" : "Name";
            this.ViewData["RatingSortParm"] = sortOrder == "Rating" ? "rating_desc" : "Rating";
            this.ViewData["CurrentSort"] = sortOrder;

            var currentPage = currPage ?? 1;

            var totalPages = await this.barService.GetPageCountAsync(5);
            var tenBars = await this.barService.GetFiveBarsAsync(currentPage, sortOrder);
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

            var barDto = await this.barService.GetBarCocktailsAsync(id);
            var model = this.barViewModelMapper.MapFrom(barDto);

            var barCommentDtos = await this.barCommentService.GetBarCommentsAsync(id);
            var barCommentVM = this.barCommentVmMapper.MapFrom(barCommentDtos);

            var barRatingDtos = await this.barRatingService.GetAllRatingsAsync(id);
            var barRatingVM = this.barRatingVmMapper.MapFrom(barRatingDtos);

            var userId = this.userManager.GetUserId(HttpContext.User);

            try
            {
                var currentUserRating = await this.barRatingService.GetRatingAsync(id, Guid.Parse(userId));
                model.CurrentUserRating = currentUserRating.Value;
            }
            catch (Exception)
            {
                model.CurrentUserRating = null;
            }        

            var cocktailsVM = this.cocktailViewModelMapper.MapFrom(barDto.Cocktails);
            
            model.Cocktails = cocktailsVM;
            
            //TODO: Fix logic here
            if (barCommentVM != null)
            {
                model.BarCommentViewModels = barCommentVM;
            }
            else
            {
                model.BarCommentViewModels = new List<BarCommentViewModel>();
            }
            if (barCommentVM != null)
            {
                model.BarRatingViewModels = barRatingVM;
            }
            else
            {
                model.BarRatingViewModels = new List<BarRatingViewModel>();
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]SearchBarViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SearchName) && model.Value == 0)
            {
                return View();
            }

            var result = await this.barService.SearchAsync(model.SearchName, model.SearchByName, model.SearchByAddress, model.SearchByRating, model.Value);
            model.SearchResults = result.Select(b => this.searchBarVmMapper.MapFrom(b)).ToList();

            return View(model);
        }
    }
}