using FluentValidation;
using Joaoaalves.QuickCQRS.Core.Tests.Fakes;

namespace Joaoaalves.QuickCQRS.Core.Tests.Fakes
{
    public class FakeCommandValidator : AbstractValidator<FakeCommand>
    {
        public FakeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
