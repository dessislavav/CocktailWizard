using CocktailWizard.Data.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailWizard.Data.Entities
{
    public class Ban : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Description { get; set; }
        public User User { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool HasExpired { get; set; }
    }
}
