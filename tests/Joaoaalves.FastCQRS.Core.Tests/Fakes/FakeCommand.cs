using Joaoaalves.FastCQRS.Abstractions.Commands;
using Joaoaalves.FastCQRS.Abstractions.Requests;

namespace Joaoaalves.FastCQRS.Core.Tests.Fakes
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