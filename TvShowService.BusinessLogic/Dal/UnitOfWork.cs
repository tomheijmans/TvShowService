using System;
using System.Threading.Tasks;
using TvShowService.BusinessLogic.Entities;

namespace TvShowService.BusinessLogic.Dal
{
    /// <summary>
    /// Unit of work context
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TvShowDbContext context;
        private GenericRepository<Actor> actorRepository;
        private GenericRepository<TvShow> tvShowRepository;

        public UnitOfWork(TvShowDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TvShow> TvShowRepository
        {
            get
            {
                if (tvShowRepository == null)
                {
                    tvShowRepository = new GenericRepository<TvShow>(context);
                }
                return tvShowRepository;
            }
        }

        public IRepository<Actor> ActorRepository
        {
            get
            {
                if (actorRepository == null)
                {
                    actorRepository = new GenericRepository<Actor>(context);
                }
                return actorRepository;
            }
        }

        /// <summary>
        /// Saves all changes on the repositories
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
