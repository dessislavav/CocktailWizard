﻿using CocktailWizard.Services;
using CocktailWizard.Web.Areas.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.ViewComponents
{
    public class CreateCocktailViewComponent : ViewComponent
    {

        private readonly IngredientService ingredientService;

        public CreateCocktailViewComponent(IngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allIngredients = await this.ingredientService.GetIngredientsAsync();
            var model = new CreateCocktailViewModel();

            model.AllAvailableIngredients = allIngredients
                    .Select(b => new SelectListItem(b.Name, b.Name))
                    .ToList();

            return View(model);

        }
    }
}
