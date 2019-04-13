using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowService.BusinessLogic.Features.SaveCast;
using TvShowService.TvMazeClient;
using TvShowService.TvMazeClient.Models;

namespace TvShowService.BusinessLogic.Features.ScrapeTvMaze
{
    /// <summary>
    /// Responsible for handling the <see cref="ScrapeTvMazeCommand"/>
    /// </summary>
    public class ScrapeTvMazeCommandHandler : ICommandHandler<ScrapeTvMazeCommand>
    {
        private readonly ITvMazeService tvMazeService;
        private readonly ICommandHandler<SaveCastCommand> saveCastCommandHandler;

        public ScrapeTvMazeCommandHandler(ITvMazeService tvMazeService, ICommandHandler<SaveCastCommand> saveCastCommandHandler)
        {
            this.tvMazeService = tvMazeService ?? throw new ArgumentNullException(nameof(tvMazeService));
            this.saveCastCommandHandler = saveCastCommandHandler ?? throw new ArgumentNullException(nameof(saveCastCommandHandler));
        }

        /// <summary>
        /// Scrapes the TvMaze API for shows and cast. Will tigger a <seealso cref="SaveCastCommand"/> for all shows
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task HandleAsync(ScrapeTvMazeCommand request)
        {
            int page = 1;
            bool reachedEnd = false;
            do
            {
                PageResult<TvShow> currentTvShows = await tvMazeService.GetShowsAsync(page);
                if (currentTvShows == null)
                {
                    // Skip something went wrong on this page, next scrape trigger will do again
                }
                else if (currentTvShows.PageExist)
                {
                    foreach (TvShow tvShow in currentTvShows.Content)
                    {
                        IList<CastMember> cast = await tvMazeService.GetShowCastAsync(tvShow.Id);
                        await saveCastCommandHandler.HandleAsync(new SaveCastCommand(tvShow, cast));
                    }
                    page++;
                }
                else
                {
                    reachedEnd = true;
                }
            }
            while (!reachedEnd);
        }
    }
}
