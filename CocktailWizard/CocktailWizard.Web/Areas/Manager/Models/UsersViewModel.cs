using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Models
{
    public class UsersViewModel
    {
        public ICollection<UserViewModel> ActiveUsers { get; set; }
        public ICollection<UserViewModel> BannedUsers { get; set; }
    }
}
