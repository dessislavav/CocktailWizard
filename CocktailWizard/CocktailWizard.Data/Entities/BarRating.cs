using CocktailWizard.Data.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailWizard.Data.Entities
{
    public class BarRating : Entity
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Please enter value between {0} and {1}")]
        public int Value { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
        public Bar Bar { get; set; }
        public Guid BarId { get; set; }
    }
}