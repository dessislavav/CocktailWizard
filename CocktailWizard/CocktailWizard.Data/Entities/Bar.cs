using CocktailWizard.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailWizard.Data.Entities
{
    public class Bar : Entity
    {
        public Bar()
        {
            this.BarCocktails = new List<BarCocktail>();
            this.Comments = new List<BarComment>();
            //this.Ingredients = new List<Ingredient>();
            this.Ratings = new List<BarRating>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Bar Name")]
        [Required]
        [StringLength(25, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }

        [DisplayName("Bar Info")]
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Info { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Address { get; set; }

        public ICollection<BarRating> Ratings { get; set; }
        public ICollection<BarComment> Comments { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        //public ICollection<Ingredient> Ingredients { get; set; }
        public string ImagePath { get; set; }

        [Required]
        [MinLength(9, ErrorMessage ="The {0} value cannot be under {1} characters.")]
        public string Phone { get; set; }
    }
}
