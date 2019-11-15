using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Areas.Member.Models;
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
            services.AddSingleton<IViewModelMapper<DetailsCocktailDto, DetailsCocktailViewModel>, DetailsCocktailViewModelMapper>();

            services.AddSingleton<IViewModelMapper<IngredientDto, IngredientViewModel>, IngredientViewModelMapper>();

            services.AddSingleton<IViewModelMapper<BarCommentDto, BarCommentViewModel>, BarCommentViewModelMapper>();
            services.AddSingleton<IViewModelMapper<CocktailCommentDto, CocktailCommentViewModel>, CocktailCommentViewModelMapper>();

            services.AddSingleton<IViewModelMapper<SearchBarDto, BarViewModel>, SearchBarViewModelMapper>();

            services.AddSingleton<IViewModelMapper<BarRatingDto, BarRatingViewModel>, BarRatingViewModelMapper>();
            services.AddSingleton<IViewModelMapper<CocktailRatingDto, CocktailRatingViewModel>, CocktailRatingViewModelMapper>();

            services.AddSingleton<IViewModelMapper<UserDto, UserViewModel>, UserViewModelMapper>();

            return services;
        }
    }
}
