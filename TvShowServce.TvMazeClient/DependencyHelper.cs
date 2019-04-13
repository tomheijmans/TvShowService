using Microsoft.Extensions.DependencyInjection;

namespace TvShowService.TvMazeClient
{
    public static class DependencyHelper
    {
        public static IServiceCollection AddTvMazeClient(this IServiceCollection services)
        {
            services.AddHttpClient<ITvMazeService, TvMazeService>();
            return services.AddScoped<ITvMazeService, TvMazeService>();
        }
    }
}
