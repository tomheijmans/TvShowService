using System.Threading.Tasks;

namespace Common.Interfaces
{
    /// <summary>
    /// Defintion for a command handler. Implementation is able to handle the given command.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Handle the given command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task HandleAsync(TCommand command);
    }
}
