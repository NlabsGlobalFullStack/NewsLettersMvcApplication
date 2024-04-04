using FluentValidation;

namespace NewsLetter.Application.Features.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(u => u.UserNameOrEmail).NotEmpty().NotNull().WithMessage("UserName Or Email Cannot be Empty");
        RuleFor(u => u.Password).NotEmpty().NotNull().WithMessage("Password Cannot be Empty");
        RuleFor(u => u.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters.");
    }
}
