using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowService.TvMazeClient.Models;

namespace TvShowService.TvMazeClient
{
    /// <summary>
    /// Service for querying the TV maze service
    /// </summary>
    public interface ITvMazeService
    {
        /// <summary>
        /// Gets the tv shows on a given page. Will query a max of 250 results.
        /// </summary>
        /// <param name="page">Number of the page to get</param>
        /// <returns>The page result for this query</returns>
        Task<PageResult<TvShow>> GetShowsAsync(int page);

        /// <summary>
        /// Gets the cast members for a given show
        /// </summary>
        /// <param name="externalTvShowId">External tv show id</param>
        /// <returns></returns>
        Task<IList<CastMember>> GetShowCastAsync(long externalTvShowId);
    }
}