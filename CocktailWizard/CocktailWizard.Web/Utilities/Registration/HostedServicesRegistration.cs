using CocktailWizard.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Utilities.Registration
{
    public static class HostedServicesRegistration
    {
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<HostedUnbanService>();

            return services;
        }
    }
}
