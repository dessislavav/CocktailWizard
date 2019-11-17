using CocktailWizard.Services;
using CocktailWizard.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailWizard.Web.Utilities.Registration
{
    public static class BusinessServicesRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IBarService, BarService>();
            services.AddScoped<ICocktailService, CocktailService>();

            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<ICocktailIngredientService, CocktailIngredientService>();

            services.AddScoped<IBarCommentService, BarCommentService>();
            services.AddScoped<ICocktailCommentService, CocktailCommentService>();

            services.AddScoped<IBarRatingService, BarRatingService>();
            services.AddScoped<ICocktailRatingService, CocktailRatingService>();

            services.AddScoped<IBanService, BanService>();

            return services;
        }
    }
}
