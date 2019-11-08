using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Data.DtoEntities
{
    public class DetailsCocktailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public ICollection<BarDto> Bars { get; set; }
        public string ImagePath { get; set; }
        public double AverageRating { get; set; }
    }
}
