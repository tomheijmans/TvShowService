using Common.Interfaces;
using System;
using System.Collections.Generic;
using TvShowService.BusinessLogic.Entities;

namespace TvShowService.BusinessLogic.Features.GetTvShows
{
    /// <summary>
    /// Query for getting the tvshows
    /// </summary>
    public class GetTvShowsQuery : IQuery<IList<TvShow>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetTvShowsQuery(int pageNumber, int pageSize)
        {
            if (pageNumber < 0)
            {
                throw new ArgumentOutOfRangeException("pageNumber");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }

            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
