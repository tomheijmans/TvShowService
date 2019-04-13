using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TvShowService.TvMazeClient.Models;

namespace TvShowService.TvMazeClient
{
    public class TvMazeService : ITvMazeService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory factory;
        private const string TvMazeBaseUrl = "http://api.tvmaze.com"; // TODO: from config
        private const int HttpStatusCodeRateLimit = 429;
        private const int RateLimitWaitSeconds = 1;

        public TvMazeService(IHttpClientFactory factory)
        {
            httpClient = factory.CreateClient();
            this.factory = factory;
        }

        public async Task<PageResult<TvShow>> GetShowsAsync(int page)
        {
            PageResult<TvShow> result = null;
            bool hitRateLimit = false;
            do
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync($"{TvMazeBaseUrl}/shows?page={page}");
                if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    result = PageResult<TvShow>.PageDoesNotExist();
                }
                else if ((int)httpResponse.StatusCode == HttpStatusCodeRateLimit)
                {
                    hitRateLimit = true;
                }
                else if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    List<TvShow> content = await httpResponse.Content.ReadAsAsync<List<TvShow>>();
                    result = PageResult<TvShow>.PageExistsWithContent(content);
                }
            }
            while (hitRateLimit);

            return result;
        }

        public async Task<IList<CastMember>> GetShowCastAsync(long tvShowId)
        {
            IList<CastMember> result = null;
            bool hitRateLimit = false;
            do
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync($"{TvMazeBaseUrl}/shows/{tvShowId}/cast");
                if ((int)httpResponse.StatusCode == HttpStatusCodeRateLimit)
                {
                    hitRateLimit = true;
                }
                else if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = await httpResponse.Content.ReadAsAsync<List<CastMember>>();
                }
            }
            while (hitRateLimit);

            return result;
        }
    }
}