using CocktailWizard.Data.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailWizard.Data.Entities
{
    public class CocktailComment : Entity
    {
        public Guid Id { get; set; }
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Text cannot exceed {1} characters")]
        public string Body { get; set; }


    }
}