using Common.Interfaces;
using System;
using System.Collections.Generic;
using TvShowService.TvMazeClient.Models;

namespace TvShowService.BusinessLogic.Features.SaveCast
{
    /// <summary>
    /// Command that states the system should store the tv show with this cast
    /// </summary>
    public class SaveCastCommand : ICommand
    {
        public TvShow TvShow { get; }
        public IEnumerable<CastMember> Cast { get; }

        public SaveCastCommand(TvShow tvShow, IEnumerable<CastMember> cast)
        {
            TvShow = tvShow ?? throw new ArgumentNullException(nameof(tvShow));
            Cast = cast ?? throw new ArgumentNullException(nameof(cast));
        }
    }
}
