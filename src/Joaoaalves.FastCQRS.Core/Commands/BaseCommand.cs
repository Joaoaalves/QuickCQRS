
using Joaoaalves.FastCQRS.Abstractions.Commands;

namespace Joaoaalves.FastCQRS.Core.Commands
{
    public class BaseCommand<TResult> : ICommand<TResult>
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}