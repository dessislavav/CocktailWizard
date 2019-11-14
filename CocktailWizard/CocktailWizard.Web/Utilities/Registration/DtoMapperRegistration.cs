using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Data.Entities;
using CocktailWizard.Services.DtoMappers;
using CocktailWizard.Services.DtoMappers.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailWizard.Web.Utilities.Registration
{
    public static class DtoMapperRegistration
    {
        public static IServiceCollection AddCustomDtoMappers(this IServiceCollection services)
        {
            services.AddSingleton<IDtoMapper<Bar, BarDto>, BarDtoMapper>();
            services.AddSingleton<IDtoMapper<Bar, SearchBarDto>,  SearchBarDtoMapper>();

            services.AddSingleton<IDtoMapper<Cocktail, CocktailDto>, CocktailDtoMapper>();
            services.AddSingleton<IDtoMapper<Cocktail, DetailsCocktailDto>, DetailsCocktailDtoMapper>();

            services.AddSingleton<IDtoMapper<Ingredient, IngredientDto>, IngredientDtoMapper>();

            services.AddSingleton<IDtoMapper<BarComment, BarCommentDto>, BarCommentDtoMapper>();
            services.AddSingleton<IDtoMapper<CocktailComment, CocktailCommentDto>, CocktailCommentDtoMapper>();

            services.AddSingleton<IDtoMapper<CocktailRating, CocktailRatingDto>, CocktailRatingDtoMapper>();

            services.AddSingleton<IDtoMapper<User, UserDto>, UserDtoMapper>();

            return services;
        }
    }
}