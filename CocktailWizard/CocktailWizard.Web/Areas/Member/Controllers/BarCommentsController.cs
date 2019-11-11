using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class BarCommentsController : Controller
    {

        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> modelMapper;
        private readonly BarCommentService barCommentService;
        private readonly UserManager<User> userManager;

        public BarCommentsController(IViewModelMapper<BarCommentDto,
            BarCommentViewModel> modelMapper,
            BarCommentService barCommentService,
            UserManager<User> userManager)
        {
            this.modelMapper = modelMapper;
            this.barCommentService = barCommentService;
            this.userManager = userManager;
        }

        // GET: BarComments
        public async Task<IActionResult> Index(Guid barId)
        {
            var barComments = await this.barCommentService.GetBarCommentsAsync(barId);
            var barCommentsVm = this.modelMapper.MapFrom(barComments);
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

        // GET: Member/BarComments/Delete
        //[HttpGet]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var barDto = await this.barCommentService.GerBarCommentAsync(id);


        //    if (barDto == null)
        //    {
        //        return NotFound();
        //    }

        //    var barCommmentVm = this.modelMapper.MapFrom(barDto);
        //    return View(barCommmentVm);
        //}

        //// POST: BarComments/Delete/
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id, Guid userId)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    if (userId == null)
        //    {
        //        return NotFound();
        //    }

        //    await this.barCommentService.DeleteAsync(id, userId);

        //    return RedirectToAction(nameof(Index));
        //}

    }
}
