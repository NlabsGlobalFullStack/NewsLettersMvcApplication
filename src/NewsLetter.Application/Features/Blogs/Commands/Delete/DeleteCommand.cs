using MediatR;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.Delete;
public sealed record DeleteCommand(Guid Id) : IRequest<Result<string>>;
