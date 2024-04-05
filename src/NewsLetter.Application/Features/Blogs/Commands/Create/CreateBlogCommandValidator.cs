using FluentValidation;

namespace NewsLetter.Application.Features.Blogs.Commands.Create;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(b => b.Title).NotEmpty().NotNull().WithMessage("Title cannot be empty");
        RuleFor(b => b.Content).NotEmpty().NotNull().WithMessage("Content cannot be empty");
    }
}