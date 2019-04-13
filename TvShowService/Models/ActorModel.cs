using System;
using TvShowService.BusinessLogic.Entities;

namespace TvShowService.Models
{
    public class ActorModel
    {
        public DateTime? DateOfBirth { get; }
        public int Id { get; }
        public string Name { get; }

        public ActorModel(Actor actor)
        {
            DateOfBirth = actor.DateOfBirth;
            Id = actor.Id;
            Name = actor.Name;
        }
    }
}
