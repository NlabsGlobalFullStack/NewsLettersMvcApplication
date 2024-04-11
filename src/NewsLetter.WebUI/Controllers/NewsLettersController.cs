using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Features.Blogs.Commands.ChangeStatus;
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

    [Authorize(AuthenticationSchemes = "Cookies")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(AuthenticationSchemes = "Cookies")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateBlogCommand request)
    {
        var response = await mediator.Send(request);

        if (response.StatusCode == 200)
        {
            TempData["Message"] = response.Data;
            TempData["status"] = "success";
            return RedirectToAction("Index");
        }
        else
        {
            TempData["Message"] = response.ErrorMessages!.First();
            TempData["status"] = "error";
            return RedirectToAction("Create");
        }
    }

    [HttpGet]
    public async Task<JsonResult> ChangeStatus(Guid id)
    {
        var response = await mediator.Send(new ChangeStatusCommand(id));
        
        if (response.StatusCode == 200)
        {
            TempData["Message"] = response.Data;
            TempData["status"] = "success";
        }
        else
        {
            TempData["Message"] = response.ErrorMessages!.First();
            TempData["status"] = "error";
        }
        return Json(true);
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



    public async Task<IActionResult> Update(GetByBlogQuery request)
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
