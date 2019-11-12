using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailWizard.Web.Areas.Member.Models
{
    public class CocktailCommentViewModel
    {
        public Guid Id { get; set; }
        public Guid CocktailId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Text cannot exceed {1} characters")]
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
