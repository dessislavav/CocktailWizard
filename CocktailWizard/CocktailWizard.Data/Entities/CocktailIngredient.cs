using CocktailWizard.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Data.Entities
{
    public class CocktailIngredient : Entity
    {
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
