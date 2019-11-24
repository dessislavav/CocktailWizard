using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CocktailRatingsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> modelMapper;
        private readonly ICocktailRatingService cocktailRatingService;

        public CocktailRatingsController(UserManager<User> userManager,
                                         IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> modelMapper,
                                         ICocktailRatingService cocktailRatingService)
        {
            this.userManager = userManager;
            this.modelMapper = modelMapper;
            this.cocktailRatingService = cocktailRatingService;
        }


        // POST: Member/CocktailRatings/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CocktailRatingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(User);
                var userName = user.Email.Split('@')[0];

                viewModel.UserId = user.Id;
                viewModel.UserName = userName;
                var ratingDto = this.modelMapper.MapFrom(viewModel);


                await this.cocktailRatingService.CreateAsync(ratingDto);

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);

        }
    }
}