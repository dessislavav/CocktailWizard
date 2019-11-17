using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public BarCommentsController(IViewModelMapper<BarCommentDto,
            BarCommentViewModel> modelMapper,
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
            //if (!ModelState.IsValid) return BusinessException

            var user = await this.userManager.GetUserAsync(User);
            var userName = user.Email.Split('@')[0];

            viewModel.UserId = user.Id;
            viewModel.UserName = userName;
            var commentDto = this.modelMapper.MapFrom(viewModel);


            await this.barCommentService.CreateAsync(commentDto);

            return Json(viewModel);

        }

        // GET: BarComments/Edit/
        public async Task<IActionResult> Edit(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var barComment = await _context.BarComments.FindAsync(id);
            //if (barComment == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // POST: BarComments/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BarComment barComment)
        {
            if (id != barComment.BarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(barComment);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!BarCommentExists(barComment.BarId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }

            return View();
        }



        // POST: BarComments/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid barId)
        {
            if (barId == null)
            {
                return NotFound();
            }

            await this.barCommentService.DeleteAsync(barId);
            this.toastNotification.AddSuccessToastMessage("Comment magically deleted");
            return RedirectToAction("Index", "Home");
        }

    }
}
