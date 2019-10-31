using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Data.Entities
{
    public class BarCocktail
    {
        public Guid BarId { get; set; }
        public Bar Bar { get; set; }
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }

    }
}
