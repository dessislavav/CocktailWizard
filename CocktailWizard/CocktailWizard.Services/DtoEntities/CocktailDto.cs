using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.DtoEntities
{
    public class CocktailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public ICollection<string> CocktailIngredients { get; set; }
        public string ImagePath { get; set; }
        public double AverageRating { get; set; }
    }
}
