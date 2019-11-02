using CocktailWizard.Data.AppContext;
using CocktailWizard.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class CocktailsController : Controller
    {
        private readonly CWContext _context;

        public CocktailsController(CWContext context)
        {
            _context = context;
        }

        // GET: Manager/Cocktails
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cocktails.ToListAsync());
        }

        // GET: Manager/Cocktails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // GET: Manager/Cocktails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manager/Cocktails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,CreatedOn,ModifiedOn,DeletedOn,IsDeleted")] Cocktail cocktail)
        {
            if (ModelState.IsValid)
            {
                cocktail.Id = Guid.NewGuid();
                _context.Add(cocktail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cocktail);
        }

        // GET: Manager/Cocktails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktails.FindAsync(id);
            if (cocktail == null)
            {
                return NotFound();
            }
            return View(cocktail);
        }

        // POST: Manager/Cocktails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ImagePath,CreatedOn,ModifiedOn,DeletedOn,IsDeleted")] Cocktail cocktail)
        {
            if (id != cocktail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cocktail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocktailExists(cocktail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cocktail);
        }

        // GET: Manager/Cocktails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // POST: Manager/Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cocktail = await _context.Cocktails.FindAsync(id);
            _context.Cocktails.Remove(cocktail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CocktailExists(Guid id)
        {
            return _context.Cocktails.Any(e => e.Id == id);
        }
    }
}
