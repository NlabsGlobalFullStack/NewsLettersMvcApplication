using MediatR;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Queries.GetById;

internal sealed class GetByIdBlogCommandHandler(IBlogRepository blogRepository) : IRequestHandler<GetByIdBlogCommand, Result<Blog>>
{
    public async Task<Result<Blog>> Handle(GetByIdBlogCommand request, CancellationToken cancellationToken)
    {

        var blog = await blogRepository.GetByExpressionAsync(b => b.Id == request.Id, cancellationToken);
        if (blog is null)
        {
            return Result<Blog>.Failure("Blog not found");
        }

        return Result<Blog>.Succeed(blog);
    }
}
