using CocktailWizard.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CocktailWizard.Services
{
    public class HostedUnbanService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private Timer timer;

        public HostedUnbanService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(UnbanService, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(30));

            return Task.CompletedTask;
        }

        private async void UnbanService(object state)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                var banService = scope.ServiceProvider.GetRequiredService<IBanService>();

                await banService.CheckForExpiredBansAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
