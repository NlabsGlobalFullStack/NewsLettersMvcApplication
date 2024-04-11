using AutoMapper;
using GenericRepository;
using MediatR;
using NewsLetter.Domain.Repositories;
using NewsLetter.Domain.Entities;
using TS.Result;
using NewsLetter.Infrastructure.Extensions;

namespace NewsLetter.Application.Features.Subscribes.Commands.CreateSubscribe;

internal sealed class CreateSubscribeCommandHandler(ISubscribeRepository subscribeRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateSubscribeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateSubscribeCommand request, CancellationToken cancellationToken)
    {
        var isBlogExists = await subscribeRepository.AnyAsync(s => s.Email == request.Email);
        if (isBlogExists)
        {
            return Result<string>.Failure("This email has been saved before.");
        }

        if (string.IsNullOrEmpty(request.Email))
        {
            return Result<string>.Failure("Email address cannot be empty!");
        }

        if (request.Email.Length < 3)
        {
            return Result<string>.Failure("Email address must be at least 3 characters!");
        }

        var email = request.Email.IsValidEmail();

        if (!email)
        {
            return Result<string>.Failure("Email address is not valid!");
        }

        Subscribe? subscribe = new()
        {
            Email = request.Email,
            EmailConfirmed = true
        };

        await subscribeRepository.AddAsync(subscribe, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Congratulations, you have successfully signed up to our follow list!");

    }
}
