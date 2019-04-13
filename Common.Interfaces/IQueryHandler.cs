using System.Threading.Tasks;

namespace Common.Interfaces
{
    /// <summary>
    /// Defintion for a query handler. Implementation is able to handle the given query.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handles the given query and returns the result
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> HandleAsync(TQuery query);
    }
}
