using MediatR;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.Update;
public sealed record UpdateBlogCommand(
    Guid Id,
    string? Title,
    string? Summary,
    string? Content,
    string? IsPublish,
    DateOnly? PublishDate
) : IRequest<Result<string>>;
