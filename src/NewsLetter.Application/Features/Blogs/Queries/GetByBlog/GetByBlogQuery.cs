using MediatR;
using NewsLetter.Domain.Entities;

namespace NewsLetter.Application.Features.Blogs.Queries.GetByBlog;
public sealed record GetByBlogQuery(string Url) : IRequest<Blog>;
