using Joaoaalves.FastCQRS.Abstractions.Requests;

namespace Joaoaalves.FastCQRS.Abstractions.Commands
{
    public interface ICommandHandler<in TCommand, TResult> :
        IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
    }
}
