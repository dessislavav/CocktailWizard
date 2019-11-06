using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailWizard.Services.Extensions
{
    public static class SearchExtensions
    {
        public static bool Contains(this string target, string[] terms)
        {
            foreach (var term in terms)
            {
                if (target.Contains(term, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
