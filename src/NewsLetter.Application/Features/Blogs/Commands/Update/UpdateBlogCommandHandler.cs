using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsLetter.Application.Extensions;
using NewsLetter.Application.Events.Blog;
using NewsLetter.Domain.Repositories;
using TS.Result;

namespace NewsLetter.Application.Features.Blogs.Commands.Update;

internal sealed class UpdateBlogCommandHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : IRequestHandler<UpdateBlogCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.Where(b => b.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (blog is null)
        {
            return Result<string>.Failure("Blog not found!");
        }

        if (request.Title is null || request.Summary is null || request.Content is null)
        {
            return Result<string>.Failure("Title | Summary | Content | fields cannot be empty!");
        }

        if (request.PublishDate is null)
        {
            return Result<string>.Failure("PublishDate must be in a valid date Format!");
        }

        var existingBlog = await blogRepository.AnyAsync(b => b.Url == request.Title!.ConvertTitleToUrl());

        if (request.Title != blog.Title)
        {
            if (existingBlog)
            {
                return Result<string>.Failure("A blog about the title has already been created, please check the information and try again!");
            }
        }

        if (request.IsPublish is not null)
        {
            blog.IsPublish = true;
        }
        else if(request.IsPublish is null)
        {
            blog.IsPublish = false;
        }

        mapper.Map(request, blog);

        blog.Url = request.Title!.ConvertTitleToUrl();

        blogRepository.Update(blog);
        await unitOfWork.SaveChangesAsync(cancellationToken);


        if (blog.IsPublish)
        {
            await mediator.Publish(new BlogEvent(blog.Id));
        }

        return Result<string>.Succeed("Update process successful.");
    }
}
