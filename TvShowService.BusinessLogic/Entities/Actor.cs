using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TvShowService.BusinessLogic.Entities
{
    public class Actor
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int TvMazeId { get; set; }
        public ICollection<ActorTvShow> ActorTvShows { get; set; }
    }
}
