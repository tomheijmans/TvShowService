using TvShowService.BusinessLogic.Entities;
using TvShowService.TvMazeClient.Models;

namespace TvShowService.BusinessLogic.Mapping
{
    public class ActorMapper : IMapper<Person, Actor>
    {
        public Actor Map(Person source)
        {
            return new Actor
            {
                TvMazeId = source.Id,
                DateOfBirth = source.Birthday,
                Name = source.Name
            };
        }
    }
}
