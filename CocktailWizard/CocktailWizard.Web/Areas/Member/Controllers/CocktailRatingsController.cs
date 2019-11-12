using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CocktailWizard.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CocktailRatingsController : Controller
    {
        private readonly CWContext context;
        private readonly UserManager<User> userManager;
        private readonly IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> modelMapper;
        private readonly CocktailRatingService cocktailRatingService;

        public CocktailRatingsController(CWContext context,
            UserManager<User> userManager,
            IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel> modelMapper,
            CocktailRatingService cocktailRatingService)
        {
            this.context = context;
            this.userManager = userManager;
            this.modelMapper = modelMapper;
            this.cocktailRatingService = cocktailRatingService;
        }

        // POST: Member/CocktailRatings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CocktailRatingViewModel viewModel)
        {
            // if model state
            var user = await this.userManager.GetUserAsync(User);
            var userName = user.Email.Split('@')[0];

            viewModel.UserId = user.Id;
            viewModel.UserName = userName;
            var commentDto = this.modelMapper.MapFrom(viewModel);


            await this.cocktailRatingService.CreateAsync(commentDto);

            return Json(viewModel);
        }
    }
}