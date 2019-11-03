using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Models
{
    public class IngredientViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Ingredient Name")]
        [Required]
        [StringLength(25, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }
    }
}
