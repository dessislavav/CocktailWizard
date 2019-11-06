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
            //this.Ratings 
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Bar Name")]
        [Required]
        [StringLength(40, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }

        [DisplayName("Bar Info")]
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Info { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Address { get; set; }

        public string ImagePath { get; set; }

        public string GoogleMapsURL { get; set; }

        [Required]
        [MinLength(9, ErrorMessage ="The {0} value cannot be under {1} characters.")]
        public string Phone { get; set; }

        public ICollection<BarRating> Ratings { get; set; } = new List<BarRating>();
        public ICollection<BarComment> Comments { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
    }
}
