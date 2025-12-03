
namespace Joaoaalves.FastCQRS.Application.Commands
{
    public class BaseCommand<TResult> : ICommand<TResult>
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}