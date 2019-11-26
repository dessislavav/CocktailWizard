using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly IViewModelMapper<DetailsCocktailDto, DetailsCocktailViewModel> detailsCocktailVmMapper;
        private readonly IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper;
        private readonly IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper;
        private readonly IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVMMapper;
        private readonly IViewModelMapper<BarDto, BarViewModel> barVmMapper;
        private readonly UserManager<User> userManager;
        private readonly ICocktailService cocktailService;
        private readonly ICocktailCommentService cocktailCommentService;
        private readonly ICocktailRatingService cocktailRatingService;
        private readonly IToastNotification toastNotification;

        public CocktailsController(ICocktailService cocktailService,
                                   IViewModelMapper<DetailsCocktailDto, DetailsCocktailViewModel> detailsCocktailVMMapper,
                                   IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> cocktailCommentVmMapper,
                                   IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> cocktailRatingVmMapper,
                                   IViewModelMapper<CocktailDto, CocktailViewModel> cocktailVmMapper,
                                   IViewModelMapper<BarDto, BarViewModel> barVmMapper,
                                   UserManager<User> userManager,
                                   ICocktailCommentService cocktailCommentService,
                                   ICocktailRatingService cocktailRatingService,
                                   IToastNotification toastNotification)
        {
            this.cocktailCommentVmMapper = cocktailCommentVmMapper ?? throw new ArgumentNullException(nameof(cocktailCommentVmMapper));
            this.detailsCocktailVmMapper = detailsCocktailVMMapper ?? throw new ArgumentNullException(nameof(detailsCocktailVMMapper));
            this.cocktailRatingVmMapper = cocktailRatingVmMapper ?? throw new ArgumentNullException(nameof(cocktailRatingVmMapper));
            this.cocktailVMMapper = cocktailVmMapper ?? throw new ArgumentNullException(nameof(cocktailVmMapper));
            this.barVmMapper = barVmMapper ?? throw new ArgumentNullException(nameof(barVmMapper));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            this.cocktailCommentService = cocktailCommentService ?? throw new ArgumentNullException(nameof(cocktailCommentService));
            this.cocktailRatingService = cocktailRatingService ?? throw new ArgumentNullException(nameof(cocktailRatingService));
            this.toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetFiveCocktails(int? currPage, string sortOrder)
        {
            try
            {
                var currentPage = currPage ?? 1;

                var totalPages = await this.cocktailService.GetPageCountAsync(5);
                var tenCocktails = await this.cocktailService.GetFiveCocktailsAsync(currentPage, sortOrder);
                var mappedTenCocktails = this.cocktailVMMapper.MapFrom(tenCocktails);

                var model = new CocktailsIndexViewModel()
                {
                    CurrPage = currentPage,
                    TotalPages = totalPages,
                    FiveCocktails = mappedTenCocktails,
                };

                if (totalPages > currentPage)
                {
                    model.NextPage = currentPage + 1;
                }

                if (currentPage > 1)
                {
                    model.PrevPage = currentPage - 1;
                }

                return PartialView("_CocktailsIndexTable", model);
            }
            catch (Exception)
            {
                this.toastNotification.AddWarningToastMessage("Something went wrong, please try again");
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: /Cocktails/Details/
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dtoCocktail = await this.cocktailService.GetCocktailBarsAsync(id);
            var model = this.detailsCocktailVmMapper.MapFrom(dtoCocktail);

            var cocktailCommentDtos = await this.cocktailCommentService.GetCocktailCommentsAsync(id);
            var cocktailCommentVM = this.cocktailCommentVmMapper.MapFrom(cocktailCommentDtos);

            var cocktailRatingDtos = await this.cocktailRatingService.GetAllRatingsAsync(id);
            var cocktailRatingVM = this.cocktailRatingVmMapper.MapFrom(cocktailRatingDtos);

            var userId = this.userManager.GetUserId(HttpContext.User);

            try
            {
                var currentUserRating = await this.cocktailRatingService.GetRatingAsync(id, Guid.Parse(userId));
                model.CurrentUserRating = currentUserRating.Value;
            }
            catch (Exception)
            {
                model.CurrentUserRating = null;
            }

            var barsVM = this.barVmMapper.MapFrom(dtoCocktail.Bars);
            model.Bars = barsVM;

            if (cocktailCommentVM != null)
            {
                model.CocktailCommentViewModels = cocktailCommentVM;
            }
            else
            {
                model.CocktailCommentViewModels = new List<CocktailCommentViewModel>();
            }

            if (cocktailRatingVM != null)
            {
                model.CocktailRatingViewModels = cocktailRatingVM;
            }
            else
            {
                model.CocktailRatingViewModels = new List<CocktailRatingViewModel>();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]SearchCocktailViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.SearchName))
                {
                    return View();
                }

                var result = await this.cocktailService.SearchAsync(model.SearchName, model.SearchByName, model.SearchByRating, model.Value);

                model.SearchResults = result
                    .Select(b => this.cocktailVMMapper.MapFrom(b))
                    .ToList();

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