using CocktailWizard.Data.Entities.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailWizard.Data.Entities
{
    public class User : IdentityUser<Guid>, IAuditable, IDeletable
    {
        public User()
        {
            this.Bans = new List<Ban>();
            this.BarComments = new List<BarComment>();
            this.BarRatings = new List<BarRating>();
            this.CocktailComments = new List<CocktailComment>();
            this.CocktailRatings = new List<CocktailRating>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public ICollection<BarComment> BarComments { get; set; }
        public ICollection<CocktailComment> CocktailComments { get; set; }
        public ICollection<BarRating> BarRatings { get; set; }
        public ICollection<CocktailRating> CocktailRatings { get; set; }
        public ICollection<Ban> Bans { get; set; }
    }
}
