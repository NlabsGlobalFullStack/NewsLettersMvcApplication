using FluentValidation;

namespace NewsLetter.Application.Features.Subscribes.Commands.CreateSubscribe;

public class CreateSubscribeCommandValidator : AbstractValidator<CreateSubscribeCommand>
{
    public CreateSubscribeCommandValidator()
    {
        RuleFor(s => s.Email).NotEmpty().NotNull().WithMessage("Email address cannot be empty!");
        RuleFor(s => s.Email).MinimumLength(3).WithMessage("Email address must be at least 3 characters!");
        RuleFor(s => s.Email).EmailAddress().WithMessage("Email address is not valid!");
    }
}
