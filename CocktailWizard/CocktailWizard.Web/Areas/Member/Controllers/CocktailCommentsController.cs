using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Member.Models;
using CocktailWizard.Web.Mappers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CocktailCommentsController : Controller
    {
        private readonly CWContext _context;
        private readonly UserManager<User> userManager;
        private readonly IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> modelMapper;
        private readonly ICocktailCommentService cocktailCommentService;
        public CocktailCommentsController(CWContext context, 
                                          UserManager<User> userManager,
                                          IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel> modelMapper,
                                          ICocktailCommentService cocktailCommentService)
        {
            _context = context;
            this.userManager = userManager;
            this.modelMapper = modelMapper;
            this.cocktailCommentService = cocktailCommentService;
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
            //if (ModelState.IsValid)

            var user = await this.userManager.GetUserAsync(User);
            var userName = user.Email.Split('@')[0];

            viewModel.UserId = user.Id;
            viewModel.UserName = userName;
            var commentDto = this.modelMapper.MapFrom(viewModel);


            await this.cocktailCommentService.CreateAsync(commentDto);

            return Json(viewModel);
        }

        // GET: Member/CocktailComments/Edit/
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktailComment = await _context.CocktailComments.FindAsync(id);
            if (cocktailComment == null)
            {
                return NotFound();
            }
            ViewData["CocktailId"] = new SelectList(_context.Cocktails, "Id", "Info", cocktailComment.CocktailId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cocktailComment.UserId);
            return View(cocktailComment);
        }

        // POST: Member/CocktailComments/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CocktailId,UserId,Body,CreatedOn,ModifiedOn,DeletedOn,IsDeleted")] CocktailComment cocktailComment)
        {
            if (id != cocktailComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cocktailComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CocktailId"] = new SelectList(_context.Cocktails, "Id", "Info", cocktailComment.CocktailId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cocktailComment.UserId);
            return View(cocktailComment);
        }

        // GET: Member/CocktailComments/Delete/
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktailComment = await _context.CocktailComments
                .Include(c => c.Cocktail)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktailComment == null)
            {
                return NotFound();
            }

            return View(cocktailComment);
        }

        // POST: Member/CocktailComments/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cocktailComment = await _context.CocktailComments.FindAsync(id);
            _context.CocktailComments.Remove(cocktailComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
