using CocktailWizard.Web.Areas.Member.Models;
using System;
using System.Collections.Generic;

namespace CocktailWizard.Web.Models
{
    public class DetailsCocktailViewModel
    {
        public DetailsCocktailViewModel()
        {
            AverageRating = 0.00;
            this.Bars = new List<BarViewModel>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public ICollection<BarViewModel> Bars { get; set; }

        public string ImagePath { get; set; }

        public double? AverageRating { get; set; }
        public ICollection<CocktailCommentViewModel> CocktailCommentViewModels { get; set; }
        public ICollection<CocktailRatingViewModel> CocktailRatingViewModels { get; set; }
        public double? CurrentUserRating { get; set; }
    }
}
