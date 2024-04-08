using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;

namespace NewsLetter.Application.Features.Blogs.Queries.GetByBlog;

internal sealed class GetByBlogQueryHandler(IBlogRepository blogRepository) : IRequestHandler<GetByBlogQuery, Blog>
{
    public async Task<Blog> Handle(GetByBlogQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.Where(b => b.Url == request.Url).FirstOrDefaultAsync(cancellationToken);
        return blog;
    }
}
