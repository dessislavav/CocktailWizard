using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Models
{
    public class CocktailViewModel
    {
        public CocktailViewModel()
        {
            AverageRating = 0.00;
            this.CocktailIngredients = new List<string>();
        }

        public Guid Id { get; set; }

        [DisplayName("Cocktail Name")]
        [Required]
        [StringLength(25, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }

        [DisplayName("Cocktail Info")]
        [Required]
        [StringLength(150, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Info { get; set; }

        public ICollection<string> CocktailIngredients { get; set; }

        public string ImagePath { get; set; }

        public double? AverageRating { get; set; }
    }
}
