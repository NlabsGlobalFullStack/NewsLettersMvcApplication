using MediatR;
using NewsLetter.Domain.Entities;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Queries.GetByBlog;
public sealed record GetByBlogQuery(string Url) : IRequest<Result<Blog>>;
