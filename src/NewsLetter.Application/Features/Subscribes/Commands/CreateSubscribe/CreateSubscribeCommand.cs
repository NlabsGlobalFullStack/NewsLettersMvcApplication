using MediatR;
using TS.Result;

namespace NewsLetter.Application.Features.Subscribes.Commands.CreateSubscribe;
public sealed record CreateSubscribeCommand(string Email) : IRequest<Result<string>>;
