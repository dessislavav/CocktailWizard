using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using CocktailWizard.Web.Models;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Services.ConstantMessages;

namespace CocktailWizard.Web.Controllers
{
    public class BarCommentsController : Controller
    {

        private readonly IViewModelMapper<BarCommentDto, BarCommentViewModel> modelMapper;
        private readonly BarCommentService barCommentService;

        public BarCommentsController(IViewModelMapper<BarCommentDto, BarCommentViewModel> modelMapper, BarCommentService barCommentService)
        {
            this.modelMapper = modelMapper;
            this.barCommentService = barCommentService;
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

        // GET: BarComments/Create
        public IActionResult Create(Guid barId)
        {
            var barVM = new BarCommentViewModel() { BarId = barId };
            return View(barVM);
        }

        // POST: BarComments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BarCommentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var tempBarComment = this.modelMapper.MapFrom(viewModel);
                var barCommentDto = await this.barCommentService.CreateAsync(tempBarComment);

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, ExceptionMessages.ModelError);
            return View(viewModel);
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

        // GET: BarComments/Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var barComment = await _context.BarComments
            //    .Include(b => b.Bar)
            //    .Include(b => b.User)
            //    .FirstOrDefaultAsync(m => m.BarId == id);
            //if (barComment == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // POST: BarComments/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var barComment = await _context.BarComments.FindAsync(id);
            //_context.BarComments.Remove(barComment);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
