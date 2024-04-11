using MediatR;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.ChangeStatus;
public sealed record ChangeStatusCommand(Guid Id) : IRequest<Result<string>>;
