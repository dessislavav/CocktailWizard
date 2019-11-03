using CocktailWizard.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailWizard.Data.Entities
{
    public class Ingredient : Entity
    {
        public Ingredient()
        {
            this.CocktailIngredients = new List<CocktailIngredient>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Ingredient Name")]
        [Required]
        [StringLength(25, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
    }
}