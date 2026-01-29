
using Joaoaalves.QuickCQRS.Abstractions.Commands;

namespace Joaoaalves.QuickCQRS.Core.Commands
{
    public class BaseCommand<TResult> : ICommand<TResult>
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}