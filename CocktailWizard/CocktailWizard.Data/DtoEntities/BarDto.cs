using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Data.DtoEntities
{
    public class BarDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
        public string ImagePath { get; set; }
        public string Phone { get; set; }
        public ICollection<CocktailDto> Cocktails { get; set; }
        public double? AverageRating { get; set; }
        public string GoogleMapsURL { get; set; }
    }
}
