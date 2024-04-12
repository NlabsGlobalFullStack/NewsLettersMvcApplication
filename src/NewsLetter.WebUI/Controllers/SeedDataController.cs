using Bogus;
using GenericRepository;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Extensions;
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
        var removeBlogs = blogRepository.GetAll().ToList();
        var removedSubscribes = subscribeRepository.GetAll().ToList();

        if (removeBlogs.Count <= 0)
        {
            return BadRequest("Silinecek blog bulunamadı.");
        }

        if (removedSubscribes.Count <= 0)
        {
            return BadRequest("Silinecek abonelik bulunamadı.");
        }

        subscribeRepository.DeleteRange(removedSubscribes);
        blogRepository.DeleteRange(removeBlogs);

        List<Blog> blogs = new();
        for (int i = 0; i < 5; i++)
        {
            Random random = new();
            Faker faker = new();
            var title = faker.Lorem.Letter(random.Next(5, 8));
            var url = title.ConvertTitleToUrl();
            Blog blog = new()
            {
                Title = title,
                Url = url,
                Summary = faker.Lorem.Letter(random.Next(15, 55)),
                Content = faker.Lorem.Lines(random.Next(3,7), "<br><br>"),
                IsPublish = (i % 2 == 0),
                PublishDate = (i % 2 == 0 ? DateOnly.FromDateTime(DateTime.Now) : null)
            };
            blogs.Add(blog);
        }
        await blogRepository.AddRangeAsync(blogs, cancellationToken);

        List<Subscribe> subscribes = new();
        foreach (var item in subscribes)
        {
            subscribes.Remove(item);
        }
        for (int i = 0; i < 20; i++)
        {
            Faker faker = new();
            Subscribe subscribe = new()
            {
                Email = faker.Person.Email,
                EmailConfirmed = true
            };

            subscribes.Add(subscribe);
        }

        await subscribeRepository.AddRangeAsync(subscribes, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Ok(new { status = "success", message = "All Datas Refreshed." });
    }
}
