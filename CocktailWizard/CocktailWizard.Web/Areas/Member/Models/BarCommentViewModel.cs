using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CocktailWizard.Web.Areas.Member.Models
{
    public class BarCommentViewModel
    {
        public Guid Id { get; set; }
        public Guid BarId { get; set; }
        public Guid UserId { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        [DisplayName("Comment Text")]
        [Required]
        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date)]
        public DateTime? ModifiedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
