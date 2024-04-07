using EntityFrameworkCorePagination.Nuget.Pagination;
using MediatR;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;

namespace NewsLetter.Application.Features.Blogs.Queries;

internal sealed class GetAllBlogsQueryHandler(IBlogRepository blogRepository) : IRequestHandler<GetAllBlogsQuery, PaginationResult<Blog>>
{
    public async Task<PaginationResult<Blog>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
    {
        var blogs =
            await blogRepository.Where(p => p.Title.ToLower().Contains(request.Search.ToLower()))
            .OrderBy(p => p.Title)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        return blogs;
    }
}
