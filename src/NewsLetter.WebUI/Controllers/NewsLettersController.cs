using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Features.Blogs.Commands.Create;
using NewsLetter.Application.Features.Blogs.Queries;

namespace NewsLetter.WebUI.Controllers;
public class NewsLettersController(IMediator mediator) : Controller
{
    public async Task<IActionResult> Index(GetAllBlogsQuery request)
    {
        var blogs = await mediator.Send(request);
        return View(blogs);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBlogCommand request)
    {
        var response = await mediator.Send(request);
        return RedirectToAction("Index");
    }
}
