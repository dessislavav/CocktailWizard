using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Mappers;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Utilities.Registration
{
    public static class ViewModelMappersRegistration
    {
        public static IServiceCollection AddCustomViewModelMappers(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelMapper<BarDto, BarViewModel>, BarViewModelMapper>();
            services.AddSingleton<IViewModelMapper<CocktailDto, CocktailViewModel>, CocktailViewModelMapper>();
            services.AddSingleton<IViewModelMapper<IngredientDto, IngredientViewModel>, IngredientViewModelMapper>();

            return services;
        }
    }
}
