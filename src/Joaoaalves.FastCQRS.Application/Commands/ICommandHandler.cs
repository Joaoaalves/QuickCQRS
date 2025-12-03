using Joaoaalves.FastCQRS.Domain.Requests;

namespace Joaoaalves.FastCQRS.Application.Commands
{
    public interface ICommandHandler<in TCommand, TResult> :
        IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
    }
}
