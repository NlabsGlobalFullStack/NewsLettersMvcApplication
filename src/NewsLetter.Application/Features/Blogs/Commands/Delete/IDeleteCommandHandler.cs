using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.Delete;
internal interface IDeleteCommandHandler
{
    Task Handle(Result<string> request, CancellationToken cancellationToken);
}