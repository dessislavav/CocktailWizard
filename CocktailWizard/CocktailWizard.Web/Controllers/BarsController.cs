using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailWizard.Data.AppContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CocktailWizard.Web.Controllers
{
    public class BarsController : Controller
    {


        public BarsController()
        {

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: /Bars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var bar = await this.context.Bars
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (bar == null)
            //{
            //    return NotFound();
            //}

            return View();
        }
    }
}