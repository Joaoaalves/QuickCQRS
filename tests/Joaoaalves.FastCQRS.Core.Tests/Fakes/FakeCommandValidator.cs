using FluentValidation;
using Joaoaalves.FastCQRS.Core.Tests.Fakes;

namespace Joaoaalves.FastCQRS.Core.Tests.Fakes
{
    public class FakeCommandValidator : AbstractValidator<FakeCommand>
    {
        public FakeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
