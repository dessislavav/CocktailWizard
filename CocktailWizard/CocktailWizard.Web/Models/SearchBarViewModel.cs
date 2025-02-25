﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Models
{
    public class SearchBarViewModel
    {
        public string SearchName { get; set; }

        public bool SearchByName { get; set; }
        public bool SearchByAddress { get; set; }
        public bool SearchByRating { get; set; }
        public double Value { get; set; }

        public IReadOnlyCollection<BarViewModel> SearchResults { get; set; } = new List<BarViewModel>();
    }
}
