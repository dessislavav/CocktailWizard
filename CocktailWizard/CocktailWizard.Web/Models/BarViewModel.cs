using CocktailWizard.Web.Areas.Member.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CocktailWizard.Web.Models
{
    public class BarViewModel
    {
        public BarViewModel()
        {
            AverageRating = 0.00;
            this.Cocktails = new List<CocktailViewModel>();
        }
        public Guid Id { get; set; }

        [DisplayName("Bar Name")]
        [Required]
        [StringLength(25, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Address { get; set; }

        [DisplayName("Bar Info")]
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Info { get; set; }
        public ICollection<CocktailViewModel> Cocktails { get; set; }

        [DisplayName("Image Path")]
        public string ImagePath { get; set; }

        public IFormFile File { get; set; }

        [Required]
        [MinLength(9, ErrorMessage = "The {0} value cannot be under {1} characters.")]
        public string Phone { get; set; }

        public double? AverageRating { get; set; }

        public string GoogleMapsURL { get; set; }

        public ICollection<BarCommentViewModel> BarCommentViewModels { get; set; }
        public ICollection<BarRatingViewModel> BarRatingViewModels { get; set; }
    }
}
