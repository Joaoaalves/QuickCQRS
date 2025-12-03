using Joaoaalves.FastCQRS.Application.Commands;
using Joaoaalves.FastCQRS.Domain.Requests;

namespace Joaoaalves.FastCQRS.Application.Tests.Fakes
{
    public class FakeCommand : IRequest<Unit>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class FakeCommandString : ICommand<string>
    {
        public Guid Id => Guid.NewGuid();
    }
}