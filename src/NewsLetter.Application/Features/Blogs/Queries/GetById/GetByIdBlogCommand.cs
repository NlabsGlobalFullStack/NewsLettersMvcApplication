using MediatR;
using NewsLetter.Domain.Entities;

namespace NewsLetter.Application.Features.Blogs.Queries.GetById;
public sealed record GetByIdBlogCommand(Guid Id) : IRequest<Blog>;
