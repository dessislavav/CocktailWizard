using CocktailWizard.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailWizard.Data.Entities
{
    public class Cocktail : Entity
    {
        public Cocktail()
        {
            this.BarCocktails = new List<BarCocktail>();
            this.CocktailIngredients = new List<CocktailIngredient>();
            this.Ratings = new List<CocktailRating>();
            this.Comments = new List<CocktailComment>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Cocktail Name")]
        [Required]
        [StringLength(40, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }

        [DisplayName("Cocktail Info")]
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Info { get; set; }

        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<CocktailRating> Ratings { get; set; }
        public ICollection<CocktailComment> Comments { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
        public string ImagePath { get; set; }
    }
}