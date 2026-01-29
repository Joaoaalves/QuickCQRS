using Joaoaalves.QuickCQRS.Abstractions.Requests;

namespace Joaoaalves.QuickCQRS.Abstractions.Commands
{
    public interface ICommand : IRequest<Unit>
    {
        Guid Id { get; }
    }

    public interface ICommand<TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }

    public readonly struct Unit
    {
        public static readonly Unit Value = new();
    }
}
