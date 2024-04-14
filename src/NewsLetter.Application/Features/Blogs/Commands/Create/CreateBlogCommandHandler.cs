using AutoMapper;
using GenericRepository;
using MediatR;
using NewsLetter.Application.Extensions;
using NewsLetter.Domain.Entities;
using NewsLetter.Application.Events.Blog;
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

        if (string.IsNullOrEmpty(request.Content) || string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Summary))
        {
            return Result<string>.Failure("Title | Summary | Content Cannot be empty");
        }

        var blog = mapper.Map<Blog>(request);
        blog.Url = request.Title.ConvertTitleToUrl();

        await blogRepository.AddAsync(blog, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (blog.IsPublish)
        {
            await mediator.Publish(new BlogEvent(blog.Id));
        }

        return Result<string>.Succeed("Blog create is successfull");
    }
}
