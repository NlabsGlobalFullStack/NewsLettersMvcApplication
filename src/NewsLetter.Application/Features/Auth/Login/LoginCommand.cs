using MediatR;
using TS.Result;

namespace NewsLetter.Application.Features.Auth.Login;
public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password,
    bool RememberMe
) : IRequest<Result<string>>;
