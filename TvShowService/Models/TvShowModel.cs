using System.Linq;
using TvShowService.BusinessLogic.Entities;

namespace TvShowService.Models
{
    public class TvShowModel
    {
        public int Id { get; }
        public string Name { get; }
        public IOrderedEnumerable<ActorModel> Actors { get; }

        public TvShowModel(TvShow show)
        {
            Id = show.Id;
            Name = show.Name;
            Actors = show.ActorTvShows?.Select(ats => new ActorModel(ats.Actor)).OrderByDescending(a => a.DateOfBirth);
        }
    }
}
