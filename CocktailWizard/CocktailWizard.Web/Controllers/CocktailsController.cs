using CocktailWizard.Data.AppContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly CWContext context;

        public CocktailsController(CWContext context)
        {
            this.context = context;
        }
        // GET: /Cocktails
        public async Task<IActionResult> Index()
        {
            return View(await this.context.Cocktails.ToListAsync());
        }

        // GET: /Cocktails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await this.context.Cocktails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

    }
}