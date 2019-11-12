using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Member.Models
{
    public class CocktailRatingViewModel
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Please enter value between {0} and {1}")]
        public double Value { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid CocktailId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
