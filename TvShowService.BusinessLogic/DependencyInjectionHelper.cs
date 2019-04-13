using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TvShowService.BusinessLogic.Dal;
using TvShowService.BusinessLogic.Entities;
using TvShowService.BusinessLogic.Features.GetTvShows;
using TvShowService.BusinessLogic.Features.SaveCast;
using TvShowService.BusinessLogic.Features.ScrapeTvMaze;
using TvShowService.BusinessLogic.Mapping;

namespace TvShowService.BusinessLogic
{
    public static class DependencyHelper
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
        {
            return services
                .AddDbContext<TvShowDbContext>(dbContextOptionsBuilder)
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ICommandHandler<SaveCastCommand>, SaveCastCommandHandler>()
                .AddScoped<ICommandHandler<ScrapeTvMazeCommand>, ScrapeTvMazeCommandHandler>()
                .AddScoped<IQueryHandler<GetTvShowsQuery, IList<TvShow>>, GetTvShowsQueryHandler>()
                .AddSingleton<IMapper<TvMazeClient.Models.TvShow, TvShow>, TvShowMapper>()
                .AddSingleton<IMapper<TvMazeClient.Models.Person, Actor>, ActorMapper>();
        }
    }
}
