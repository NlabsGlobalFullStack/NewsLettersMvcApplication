using Bogus;
using GenericRepository;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;

namespace NewsLetter.WebUI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class SeedDataController(ISubscribeRepository subscribeRepository, IBlogRepository blogRepository, IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Seed(CancellationToken cancellationToken)
    {
        Faker faker = new();
        List<Blog> blogs = new();
        for (int i = 0; i < 5; i++)
        {
            Random random = new();
            Blog blog = new()
            {
                Title = faker.Lorem.Letter(random.Next(5, 8)),
                Summary = faker.Lorem.Letter(random.Next(15, 55)),
                Content = faker.Lorem.Lines(random.Next(3,7), "<br><br>"),
                IsPublish = (i % 2 == 0),
                PublishDate = (i % 2 == 0 ? DateOnly.FromDateTime(DateTime.Now) : null)
            };
            blogs.Add(blog);
        }

        await blogRepository.AddRangeAsync(blogs, cancellationToken);

        List<Subscribe> subscribes = new();
        for (int i = 0; i < 1000; i++)
        {
            Subscribe subscribe = new()
            {
                Email = faker.Person.Email,
                EmailConfirmed = true
            };

            subscribes.Add(subscribe);
        }

        await subscribeRepository.AddRangeAsync(subscribes, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
