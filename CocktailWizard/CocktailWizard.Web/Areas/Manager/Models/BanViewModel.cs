using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Models
{
    public class BanViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Ban Reason")]
        [Required(ErrorMessage = "You must include a description for the ban")]
        [MinLength(3)]
        [MaxLength(80)]
        public string Description { get; set; }
        public string UserId { get; set; }

        [DisplayName("Ban Period (in weeks)")]
        [Required(ErrorMessage = "Please provide a period between 1 and 12(months)")]
        [Range(1, 12)]
        public int Period { get; set; }
    }
}
