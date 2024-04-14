using GenericRepository;
using MediatR;
using NewsLetter.Application.Events.Blog;
using NewsLetter.Domain.Repositories;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.ChangeStatus;

internal sealed class ChangeStatusCommandHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork, IMediator mediator) : IRequestHandler<ChangeStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByExpressionWithTrackingAsync(b => b.Id == request.Id, cancellationToken);
        if (blog is null)
        {
            return Result<string>.Failure("Blog not found");
        }

        blog.IsPublish = !blog.IsPublish;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (blog.IsPublish)
        {
            await mediator.Publish(new BlogEvent(request.Id));

        }

        return Result<string>.Succeed("Change status is successful");
    }
}
