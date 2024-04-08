using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Features.Blogs.Commands.Create;
using NewsLetter.Application.Features.Blogs.Queries.GetAll;
using NewsLetter.Application.Features.Blogs.Queries.GetByBlog;

namespace NewsLetter.WebUI.Controllers;
public class NewsLettersController(IMediator mediator) : Controller
{
    public async Task<IActionResult> Index(GetAllBlogsQuery request)
    {
        var blogs = await mediator.Send(request);
        return View(blogs);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBlogCommand request)
    {
        var response = await mediator.Send(request);
        return RedirectToAction("Index", response);
    }

    public async Task<IActionResult> Detail(GetByBlogQuery request)
    {
        var blog = await mediator.Send(request);
        if (blog is null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(blog);
        }
    }
}
