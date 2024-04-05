using MediatR;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.Create;
public sealed record CreateBlogCommand(
    string Title,
    string Content,
    bool IsPublish
) : IRequest<Result<string>>;
