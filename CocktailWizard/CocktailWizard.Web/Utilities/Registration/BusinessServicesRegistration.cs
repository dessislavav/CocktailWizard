using CocktailWizard.Services;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<BarCommentService>();
            services.AddScoped<CocktailCommentService>();
            services.AddScoped<CocktailRatingService>();
            services.AddScoped<BanService>();

            return services;
        }
    }
}
