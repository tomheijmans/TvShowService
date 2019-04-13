using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TvShowService.BusinessLogic.Entities
{
    public class TvShow
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public long TvMazeId { get; set; }
        public ICollection<ActorTvShow> ActorTvShows { get; set; }
    }
}
