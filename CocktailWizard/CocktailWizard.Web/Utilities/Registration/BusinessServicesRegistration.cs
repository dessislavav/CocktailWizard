using CocktailWizard.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Utilities.Registration
{
    public static class BusinessServicesRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<BarService>();
            services.AddScoped<CocktailService>();
            services.AddScoped<IngredientService>();
            services.AddScoped<CocktailIngredientService>();

            return services;
        }
    }
}
