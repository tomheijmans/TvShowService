using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvShowService.BusinessLogic.Features.GetTvShows;
using TvShowService.Models;

namespace TvShowService.Controllers
{
    /// <summary>
    /// Controller to list all tvshows
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TvShowController : ControllerBase
    {
        private readonly IQueryHandler<GetTvShowsQuery, IList<BusinessLogic.Entities.TvShow>> queryHandler;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="queryHandler"></param>
        public TvShowController(IQueryHandler<GetTvShowsQuery, IList<BusinessLogic.Entities.TvShow>> queryHandler)
        {
            this.queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
        }

        /// <summary>
        /// Returns a list of tv shows with their actors
        /// </summary>
        /// <param name="pageNumber">Number of the page to get</param>
        /// <param name="pageSize">Size of the page to get</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TvShowModel>), 200)]
        public async Task<IEnumerable<TvShowModel>> GetAsync([FromQuery]int pageNumber, [FromQuery]int pageSize = 100)
        {
            IList<BusinessLogic.Entities.TvShow> result = await queryHandler.HandleAsync(new GetTvShowsQuery(pageNumber, pageSize));
            return result.Select(res => new TvShowModel(res));
        }
    }
}
