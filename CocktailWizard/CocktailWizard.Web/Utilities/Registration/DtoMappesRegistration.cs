using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailWizard.Web.Utilities.Registration
{
    public static class DtoMappesRegistration
    {
        public static IServiceCollection AddCustomDtoMappers(this IServiceCollection services)
        {
            services.AddSingleton<IDtoMapper<Bar, BarDto>, BarDtoMapper>();
            services.AddSingleton<IDtoMapper<Bar, SearchBarDto>,  SearchBarDtoMapper>();

            services.AddSingleton<IDtoMapper<Cocktail, CocktailDto>, CocktailDtoMapper>();
            services.AddSingleton<IDtoMapper<Cocktail, DetailsCocktailDto>, DetailsCocktailDtoMapper>();

            services.AddSingleton<IDtoMapper<Ingredient, IngredientDto>, IngredientDtoMapper>();

            services.AddSingleton<IDtoMapper<BarComment, BarCommentDto>, BarCommentDtoMapper>();
            services.AddSingleton<IDtoMapper<BarComment, BarCommentDto>, BarCommentDtoMapper>();

            return services;
        }
    }
}