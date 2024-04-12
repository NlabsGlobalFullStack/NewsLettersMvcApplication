using GenericRepository;
using MediatR;
using NewsLetter.Domain.Repositories;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.Delete;
public sealed record DeleteCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class DeleteCommandHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByExpressionWithTrackingAsync(b => b.Id == request.Id, cancellationToken);
        if (blog is null)
        {
            return Result<string>.Failure("Blog not found");
        }
        blogRepository.Delete(blog);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Deletion successful!");
    }
}
