using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsBanned { get; set; }
        [DisplayName("Ban Reason")]
        public string ReasonForBan { get; set; }
    }
}
