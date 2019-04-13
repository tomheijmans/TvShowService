using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvShowService.BusinessLogic.Dal;
using TvShowService.BusinessLogic.Mapping;

namespace TvShowService.BusinessLogic.Features.SaveCast
{
    /// <summary>
    /// Responsible for handling the save command. Will save cast data to the repository
    /// </summary>
    public class SaveCastCommandHandler : ICommandHandler<SaveCastCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper<TvMazeClient.Models.TvShow, Entities.TvShow> tvShowMapper;
        private readonly IMapper<TvMazeClient.Models.Person, Entities.Actor> actorMapper;

        public SaveCastCommandHandler(IUnitOfWork unitOfWork,
            IMapper<TvMazeClient.Models.TvShow, Entities.TvShow> tvShowMapper,
            IMapper<TvMazeClient.Models.Person, Entities.Actor> actorMapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.tvShowMapper = tvShowMapper ?? throw new ArgumentNullException(nameof(tvShowMapper));
            this.actorMapper = actorMapper ?? throw new ArgumentNullException(nameof(actorMapper));
        }

        /// <summary>
        /// Executes the save command
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task HandleAsync(SaveCastCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Entities.TvShow tvShow = unitOfWork.TvShowRepository.Get(
                predicate: show => show.TvMazeId == request.TvShow.Id,
                includeProperties: "ActorTvShows.TvShow")
                .SingleOrDefault();
            if (tvShow == null)
            {
                tvShow = tvShowMapper.Map(request.TvShow);
                tvShow.ActorTvShows = new List<Entities.ActorTvShow>();
                foreach (TvMazeClient.Models.Person castMember in request.Cast.Select(cast => cast.Person))
                {
                    // Sometimes there are duplicates. We only want unique persons 
                    if (!tvShow.ActorTvShows.Any(ats => ats.Actor.TvMazeId == castMember.Id))
                    {
                        tvShow.ActorTvShows.Add(new Entities.ActorTvShow
                        {
                            TvShow = tvShow,
                            Actor = FindOrCreateActor(castMember)
                        });
                    }
                }

                unitOfWork.TvShowRepository.Add(tvShow);
            }
            else
            {
                tvShow.ActorTvShows.Clear();
                foreach (TvMazeClient.Models.Person castMember in request.Cast.Select(cast => cast.Person))
                {
                    tvShow.ActorTvShows.Add(new Entities.ActorTvShow
                    {
                        TvShow = tvShow,
                        Actor = FindOrCreateActor(castMember)
                    });
                }

                unitOfWork.TvShowRepository.Update(tvShow);
            }

            await unitOfWork.SaveChangesAsync();
        }

        private Entities.Actor FindOrCreateActor(TvMazeClient.Models.Person person)
        {
            Entities.Actor actor = unitOfWork.ActorRepository.Get(a => a.TvMazeId == person.Id).SingleOrDefault();
            if (actor == null)
            {
                actor = actorMapper.Map(person);
            }
            return actor;
        }
    }
}
