using AutoMapper;
using GenericRepository;
using MediatR;
using NewsLetter.Application.Extensions;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Events.Auth;
using NewsLetter.Domain.Repositories;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.Create;

internal sealed class CreateBlogCommandHandler(IBlogRepository blogRepository, IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork) : IRequestHandler<CreateBlogCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var isBlogExists = await blogRepository.AnyAsync(b => b.Title == request.Title, cancellationToken);
        if (isBlogExists)
        {
            return Result<string>.Failure("This blog has been previously saved");
        }

        var blog = mapper.Map<Blog>(request);
        blog.Url = request.Title.ConvertTitleToUrl();

        await blogRepository.AddAsync(blog, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (blog.IsPublish)
        {
            await mediator.Publish(new BlogEvent(blog.Id));
        }

        return "Blog create is successfull";
    }
}
