using EntityFrameworkCorePagination.Nuget.Pagination;
using MediatR;
using NewsLetter.Domain.Entities;

namespace NewsLetter.Application.Features.Blogs.Queries;
public sealed record GetAllBlogsQuery(
     int PageNumber = 1,
    int PageSize = 2,
    string Search = ""
) : IRequest<PaginationResult<Blog>>;
