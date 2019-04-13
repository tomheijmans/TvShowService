using Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TvShowService.BusinessLogic.Features.ScrapeTvMaze;

namespace TvShowService.HostedServices
{
    /// <summary>
    /// Scrape service. Will trigger the scrape command and will run endless starting every 5 seconds
    /// </summary>
    public class ScrapeService : BackgroundService
    {
        private readonly IServiceProvider services;

        public ScrapeService(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Executs the scrape job
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = services.CreateScope())
                {
                    ICommandHandler<ScrapeTvMazeCommand> scrapeTvMazeHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<ScrapeTvMazeCommand>>();
                    await scrapeTvMazeHandler.HandleAsync(new ScrapeTvMazeCommand());
                }

                await Task.Delay(5000); // TODO Configurable maken hoe vaak deze moet runnen.
            }
        }
    }
}
