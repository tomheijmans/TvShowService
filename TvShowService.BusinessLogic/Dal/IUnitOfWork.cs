using System.Threading.Tasks;
using TvShowService.BusinessLogic.Entities;

namespace TvShowService.BusinessLogic.Dal
{
    public interface IUnitOfWork
    {
        IRepository<Actor> ActorRepository { get; }
        IRepository<TvShow> TvShowRepository { get; }
        Task<int> SaveChangesAsync();
    }
}