using CocktailWizard.Data.Entities.Abstract;
using CocktailWizard.Data.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Data.Entities
{
    public class CocktailIngredient : IDeletable
    {
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
