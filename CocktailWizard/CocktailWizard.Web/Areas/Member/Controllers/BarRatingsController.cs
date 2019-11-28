using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.DtoEntities;
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
    public class BarRatingsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IViewModelMapper<BarRatingDto, BarRatingViewModel> modelMapper;
        private readonly IBarRatingService barRatingService;

        public BarRatingsController(UserManager<User> userManager,
                                    IViewModelMapper<BarRatingDto, BarRatingViewModel> modelMapper,
                                    IBarRatingService barRatingService)
        {
            this.userManager = userManager;
            this.modelMapper = modelMapper;
            this.barRatingService = barRatingService;
        }


        // POST: Member/BarRatings/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarRatingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(User);
                var userName = user.Email.Split('@')[0];

                viewModel.UserId = user.Id;
                viewModel.UserName = userName;
                var ratingDto = this.modelMapper.MapFrom(viewModel);


                await this.barRatingService.CreateAsync(ratingDto);

                return RedirectToAction("Details", "Bars", new { id = viewModel.BarId });
            }

            return View(viewModel);

        }
    }
}