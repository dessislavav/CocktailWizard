using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Mappers;
using CocktailWizard.Web.Mappers.Contracts;
using CocktailWizard.Web.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailWizard.Web.Utilities.Registration
{
    public static class ViewModelMappersRegistration
    {
        public static IServiceCollection AddCustomViewModelMappers(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelMapper<BarDto, BarViewModel>, BarViewModelMapper>();
            services.AddSingleton<IViewModelMapper<CocktailDto, CocktailViewModel>, CocktailViewModelMapper>();
            services.AddSingleton<IViewModelMapper<IngredientDto, IngredientViewModel>, IngredientViewModelMapper>();
            services.AddSingleton<IViewModelMapper<BarCommentDto, BarCommentViewModel>, BarCommentViewModelMapper>();
            services.AddSingleton<IViewModelMapper<SearchBarDto, BarViewModel>, SearchBarViewModelMapper>();

            return services;
        }
    }
}
