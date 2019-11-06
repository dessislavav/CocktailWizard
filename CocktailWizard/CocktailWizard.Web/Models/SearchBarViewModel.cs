using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Models
{
    public class SearchBarViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Please provide at least 3 letters")]
        public string SearchName { get; set; }

        public bool SearchByName { get; set; }
        public bool SearchByAddress { get; set; }
        public bool SearchByRating { get; set; }

        public IReadOnlyCollection<BarViewModel> SearchResults { get; set; } = new List<BarViewModel>();
    }
}
