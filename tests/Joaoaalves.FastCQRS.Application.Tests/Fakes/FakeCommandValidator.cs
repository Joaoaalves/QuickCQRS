using FluentValidation;

namespace Joaoaalves.FastCQRS.Application.Tests.Fakes
{
    public class FakeCommandValidator : AbstractValidator<FakeCommand>
    {
        public FakeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
