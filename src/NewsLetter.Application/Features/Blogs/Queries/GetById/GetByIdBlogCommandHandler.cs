using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;

namespace NewsLetter.Application.Features.Blogs.Queries.GetById;

internal sealed class GetByIdBlogCommandHandler(IBlogRepository blogRepository) : IRequestHandler<GetByIdBlogCommand, Blog>
{
    public async Task<Blog> Handle(GetByIdBlogCommand request, CancellationToken cancellationToken)
    {

        var blog = await blogRepository.Where(b => b.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        return blog!;
    }
}
