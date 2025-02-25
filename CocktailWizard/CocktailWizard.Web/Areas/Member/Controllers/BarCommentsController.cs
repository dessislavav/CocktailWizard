﻿using CocktailWizard.Data.Entities;
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

namespace CocktailWizard.Web.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class BarCommentsController : Controller
    {

        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> modelMapper;
        private readonly IBarCommentService barCommentService;
        private readonly UserManager<User> userManager;
        private readonly IToastNotification toastNotification;

        public BarCommentsController(IViewModelMapper<BarCommentDto, BarCommentViewModel> modelMapper,
                                     IBarCommentService barCommentService,
                                     UserManager<User> userManager,
                                     IToastNotification toastNotification)
        {
            this.modelMapper = modelMapper;
            this.barCommentService = barCommentService;
            this.userManager = userManager;
            this.toastNotification = toastNotification;
        }

        // GET: BarComments
        public async Task<IActionResult> Index(Guid barId)
        {
            if (barId == null)
            {
                return NotFound();
            }

            var barCommentDtos = await this.barCommentService.GetBarCommentsAsync(barId);
            var barCommentsVm = this.modelMapper.MapFrom(barCommentDtos);
            return View(barCommentsVm);
        }

        // GET: BarComments/Details
        public async Task<IActionResult> Details(Guid barId)
        {
            if (barId == null)
            {
                return NotFound();
            }

            var barCommentDtos = await this.barCommentService.GetBarCommentsAsync(barId);
            var barCommentVM = this.modelMapper.MapFrom(barCommentDtos);

            return View(barCommentVM);
        }

        // POST: BarComments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]BarCommentViewModel viewModel)
        {

            try
            {
                var user = await this.userManager.GetUserAsync(User);
                var userName = user.Email.Split('@')[0];

                viewModel.UserId = user.Id;
                viewModel.UserName = userName;
                var commentDto = this.modelMapper.MapFrom(viewModel);

                var newCommentDto = await this.barCommentService.CreateAsync(commentDto);
                var newCommentVm = this.modelMapper.MapFrom(newCommentDto);

                this.toastNotification.AddSuccessToastMessage("Comment magically posted.");

                return PartialView("_AddBarCommentPartial", newCommentVm);
            }
            catch (Exception)
            {
                this.toastNotification.AddErrorToastMessage("Text must be between 2 and 500 symbols.");
            }

            return View(viewModel);
        }


        // POST: BarComments/Edit/
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
                    await this.barCommentService.EditAsync(id, newBody);
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



        // POST: BarComments/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Guid barId)
        {
            if (barId == null || id == null)
            {
                return NotFound();
            }

            await this.barCommentService.DeleteAsync(id, barId);
            this.toastNotification.AddSuccessToastMessage("Comment magically deleted.");

            return RedirectToAction("Index", "Home");
        }

    }
}
