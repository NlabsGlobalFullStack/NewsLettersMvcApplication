using MediatR;
using NewsLetter.Domain.Entities;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Queries.GetById;
public sealed record GetByIdBlogCommand(Guid Id) : IRequest<Result<Blog>>;
