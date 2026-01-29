using Joaoaalves.QuickCQRS.Abstractions.Commands;
using Joaoaalves.QuickCQRS.Abstractions.Requests;

namespace Joaoaalves.QuickCQRS.Core.Tests.Fakes
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