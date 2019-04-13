namespace TvShowService.BusinessLogic.Entities
{
    public class ActorTvShow
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public int TvShowId { get; set; }
        public TvShow TvShow { get; set; }
    }
}
