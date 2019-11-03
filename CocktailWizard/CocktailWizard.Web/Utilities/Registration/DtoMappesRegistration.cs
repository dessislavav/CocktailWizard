using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Utilities.Registration
{
    public static class DtoMappesRegistration
    {
        public static IServiceCollection AddCustomDtoMappers(this IServiceCollection services)
        {
            services.AddSingleton<IDtoMapper<Bar, BarDto>, BarDtoMapper>();
            services.AddSingleton<IDtoMapper<Cocktail, CocktailDto>, CocktailDtoMapper>();
            services.AddSingleton<IDtoMapper<Ingredient, IngredientDto>, IngredientDtoMapper>();

            return services;
        }
    }
}
