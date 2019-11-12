using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailWizard.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CocktailRatingsController : Controller
    {
        public CocktailRatingsController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}