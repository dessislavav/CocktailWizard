using CocktailWizard.Data.Entities;
using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Services.CustomExceptions;
using CocktailWizard.Services.DtoEntities;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CocktailCommentsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> modelMapper;
        private readonly ICocktailCommentService cocktailCommentService;
        private readonly IToastNotification toastNotification;

        public CocktailCommentsController(UserManager<User> userManager,
                                          IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> modelMapper,
                                          ICocktailCommentService cocktailCommentService,
                                          IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.modelMapper = modelMapper;
            this.cocktailCommentService = cocktailCommentService;
            this.toastNotification = toastNotification;
        }

        // GET: Member/CocktailComments
        public async Task<IActionResult> Index(Guid cocktailId)
        {
            if (cocktailId == null)
            {
                return NotFound();
            }

            var cocktailCommentDtos = await this.cocktailCommentService.GetCocktailCommentsAsync(cocktailId);
            var cocktailCommentsVm = this.modelMapper.MapFrom(cocktailCommentDtos);

            return View(cocktailCommentsVm);
        }

        // GET: Member/CocktailComments/Details/
        public async Task<IActionResult> Details(Guid cocktailId)
        {
            if (cocktailId == null)
            {
                return NotFound();
            }

            var cocktailCommentDtos = await this.cocktailCommentService.GetCocktailCommentsAsync(cocktailId);
            var cocktailCommentsVm = this.modelMapper.MapFrom(cocktailCommentDtos);

            return View(cocktailCommentsVm);
        }

        // POST: Member/CocktailComments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]CocktailCommentViewModel viewModel)
        {
            try
            {
                var user = await this.userManager.GetUserAsync(User);
                var userName = user.Email.Split('@')[0];

                viewModel.UserId = user.Id;
                viewModel.UserName = userName;
                var commentDto = this.modelMapper.MapFrom(viewModel);

                var newCommentDto = await this.cocktailCommentService.CreateAsync(commentDto);
                var newCommentVm = this.modelMapper.MapFrom(newCommentDto);

                this.toastNotification.AddSuccessToastMessage("Comment magically posted.");

                return PartialView("_AddCocktailCommentPartial", newCommentVm);
            }
            catch (Exception)
            {

                this.toastNotification.AddErrorToastMessage("Text must be between 2 and 500 symbols.");
            }

            return View(viewModel);
        }

        // POST: Member/CocktailComments/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string newBody)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.cocktailCommentService.EditAsync(id, newBody);
                }
                catch (BusinessLogicException)
                {
                    throw new BusinessLogicException(ExceptionMessages.GeneralOopsMessage);
                }
                this.toastNotification.AddSuccessToastMessage("Comment magically edited.");
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }


        // POST: Member/CocktailComments/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Guid cocktailId)
        {
            if (cocktailId == null || id == null)
            {
                return NotFound();
            }

            await this.cocktailCommentService.DeleteAsync(id, cocktailId);
            this.toastNotification.AddSuccessToastMessage("Comment magically deleted.");

            return RedirectToAction("Index", "Home");
        }


    }
}
