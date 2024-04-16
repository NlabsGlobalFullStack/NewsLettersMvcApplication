using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Features.Blogs.Commands.ChangeStatus;
using NewsLetter.Application.Features.Blogs.Commands.Create;
using NewsLetter.Application.Features.Blogs.Commands.Delete;
using NewsLetter.Application.Features.Blogs.Commands.Update;
using NewsLetter.Application.Features.Blogs.Queries.GetAll;
using NewsLetter.Application.Features.Blogs.Queries.GetByBlog;
using NewsLetter.Application.Features.Blogs.Queries.GetById;
using NewsLetter.WebUI.Helpers;

namespace NewsLetter.WebUI.Controllers;
public class NewsLettersController : BaseController
{
    public NewsLettersController(IMediator mediator) : base(mediator) {}

    public async Task<IActionResult> Index(GetAllBlogsQuery request)
    {
        var blogs = await _mediator.Send(request);
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
        var response = await _mediator.Send(request);

        MyHelper.Handle(this, response, out result);
        if (result == 200)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Create");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies")]
    [HttpGet]
    public async Task<JsonResult> ChangeStatus(Guid id)
    {
        var response = await _mediator.Send(new ChangeStatusCommand(id));

        MyHelper.Handle(this, response, out result);

        if (result == 200)
        {
            return Json(true);
        }
        else
        {
            return Json(false);
        }
    }

    public async Task<IActionResult> Detail(GetByBlogQuery request)
    {
        var response = await _mediator.Send(request);

        if (response.Data is null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(response);
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies")]
    public async Task<IActionResult> Edit(GetByIdBlogCommand request)
    {      
        var response = await _mediator.Send(request);

        if (response.Data is not null)
        {
            return View("Update", response);
        }
        else
        {
            return RedirectToAction("Index");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies")]
    [HttpPost]
    public async Task<IActionResult> UpdatePost(UpdateBlogCommand request)
    {
        var response = await _mediator.Send(request);

        MyHelper.Handle(this, response, out result);
        if (result == 200)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Update");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies")]
    [HttpGet]
    public async Task<JsonResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteCommand(id));

        MyHelper.Handle(this, response, out result, "warning");

        if (result == 200)
        {
            return Json(true);
        }
        else
        {
            return Json(false);
        }

    }
}
