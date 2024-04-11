using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsLetter.Domain.Entities;
using TS.Result;

namespace NewsLetter.Application.Features.Auth.Login;

internal sealed class LoginCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == request.UserNameOrEmail || u.Email == request.UserNameOrEmail, cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure( "User not found!");
        }

        var checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
        if (!checkPassword)
        {
            return Result<string>.Failure( "Password is wrong!");
        }

        return Result<string>.Succeed("Login is successull");
    }
}
