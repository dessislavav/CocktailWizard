using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CocktailWizard.Web.Models
{
    public class BarCommentViewModel
    {
        public Guid BarId { get; set; }
        //public Bar Bar { get; set; }
        public Guid UserId { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        [DisplayName("Comment Text")]
        [Required]
        [StringLength(500, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Body { get; set; }
    }
}
