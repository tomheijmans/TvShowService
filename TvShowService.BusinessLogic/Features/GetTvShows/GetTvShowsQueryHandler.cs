using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowService.BusinessLogic.Dal;
using TvShowService.BusinessLogic.Entities;

namespace TvShowService.BusinessLogic.Features.GetTvShows
{
    /// <summary>
    /// Responsible for handling the <see cref="GetTvShowsQuery"></see>
    /// </summary>
    public class GetTvShowsQueryHandler : IQueryHandler<GetTvShowsQuery, IList<TvShow>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTvShowsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Handles the <seealso cref="GetTvShowsQuery"/>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<IList<TvShow>> HandleAsync(GetTvShowsQuery query)
        {
            return Task.FromResult(unitOfWork.TvShowRepository.Get(
                skip: query.PageNumber * query.PageSize,
                take: query.PageSize,
                includeProperties: "ActorTvShows.Actor"));
        }
    }
}
