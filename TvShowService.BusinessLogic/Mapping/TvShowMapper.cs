namespace TvShowService.BusinessLogic.Mapping
{
    public class TvShowMapper : IMapper<TvMazeClient.Models.TvShow, Entities.TvShow>
    {
        public Entities.TvShow Map(TvMazeClient.Models.TvShow source)
        {
            return new Entities.TvShow
            {
                TvMazeId = source.Id,
                Name = source.Name
            };
        }
    }
}
