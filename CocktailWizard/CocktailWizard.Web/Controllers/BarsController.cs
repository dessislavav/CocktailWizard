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
using NToastNotify;
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
        private readonly IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper;
        private readonly UserManager<User> userManager;
        private readonly IBarCommentService barCommentService;
        private readonly IBarRatingService barRatingService;
        private readonly IBarService barService;
        private readonly IToastNotification toastNotification;

        public BarsController(IViewModelMapper<BarDto, BarViewModel> barViewModelMapper,
                              IViewModelMapper<SearchBarDto, BarViewModel> searchBarVmMapper,
                              IViewModelMapper<BarCommentDto, BarCommentViewModel> barCommentVmMapper,
                              IViewModelMapper<BarRatingDto, BarRatingViewModel> barRatingVmMapper,
                              IViewModelMapper<CocktailDto, CocktailViewModel> cocktailViewModelMapper,
                              UserManager<User> userManager,
                              IBarCommentService barCommentService,
                              IBarRatingService barRatingService,
                              IBarService barService,
                              IToastNotification toastNotification)
        {
            this.barViewModelMapper = barViewModelMapper ?? throw new ArgumentNullException(nameof(barViewModelMapper));
            this.cocktailViewModelMapper = cocktailViewModelMapper ?? throw new ArgumentNullException(nameof(cocktailViewModelMapper));
            this.searchBarVmMapper = searchBarVmMapper ?? throw new ArgumentNullException(nameof(searchBarVmMapper));
            this.barRatingVmMapper = barRatingVmMapper ?? throw new ArgumentNullException(nameof(barRatingVmMapper));
            this.barCommentVmMapper = barCommentVmMapper ?? throw new ArgumentNullException(nameof(searchBarVmMapper));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.barService = barService ?? throw new ArgumentNullException(nameof(barService));
            this.toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
            this.barCommentService = barCommentService ?? throw new ArgumentNullException(nameof(barCommentService));
            this.barRatingService = barRatingService ?? throw new ArgumentNullException(nameof(barRatingService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetFiveBars(int? currPage, string sortOrder)
        {
            try
            {
                var currentPage = currPage ?? 1;

                var totalPages = await this.barService.GetPageCountAsync(5);
                var tenBars = await this.barService.GetFiveBarsAsync(currentPage, sortOrder);
                var mappedTenBars = this.barViewModelMapper.MapFrom(tenBars);

                var model = new BarsIndexViewModel()
                {
                    CurrPage = currentPage,
                    TotalPages = totalPages,
                    FiveBars = mappedTenBars,
                };

                if (totalPages > currentPage)
                {
                    model.NextPage = currentPage + 1;
                }

                if (currentPage > 1)
                {
                    model.PrevPage = currentPage - 1;
                }

                return PartialView("_BarsIndexTable", model);
            }
            catch (Exception)
            {
                this.toastNotification.AddWarningToastMessage("Something went wrong, please try again");
                return RedirectToAction(nameof(Index));
            }
        }

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

            if (barCommentVM != null)
            {
                model.BarCommentViewModels = barCommentVM;
            }
            else
            {
                model.BarCommentViewModels = new List<BarCommentViewModel>();
            }
            if (barRatingVM != null)
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
            try
            {
                if (string.IsNullOrWhiteSpace(model.SearchName) && model.Value == 0)
                {
                    return View();
                }

                var result = await this.barService.SearchAsync(model.SearchName, model.SearchByName, model.SearchByAddress, model.SearchByRating, model.Value);
                model.SearchResults = result.Select(b => this.searchBarVmMapper.MapFrom(b)).ToList();

                return View(model);
            }
            catch (Exception)
            {
                this.toastNotification.AddWarningToastMessage("Something went wrong, please try again");
                return View();
            }
        }
    }
}