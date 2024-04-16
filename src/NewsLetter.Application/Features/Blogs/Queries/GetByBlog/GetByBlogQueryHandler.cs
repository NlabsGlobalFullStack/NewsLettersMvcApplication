using MediatR;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Queries.GetByBlog;

internal sealed class GetByBlogQueryHandler(IBlogRepository blogRepository) : IRequestHandler<GetByBlogQuery, Result<Blog>>
{
    public async Task<Result<Blog>> Handle(GetByBlogQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByExpressionAsync(b => b.Url == request.Url, cancellationToken);
        if (blog is null)
        {
            return Result<Blog>.Failure("Blog not found");
        }
        return Result<Blog>.Succeed(blog);
    }
}
