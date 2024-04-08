using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Features.Subscribes.Commands.CreateSubscribe;

namespace NewsLetter.WebUI.Controllers;
public class HomeController(IMediator mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(string email)
    {
        var response = await mediator.Send(new CreateSubscribeCommand(email));
        if (response.StatusCode == 200)
        {
            TempData["Message"] = response.Data;
        }
        else
        {
            TempData["Message"] = response.Data;
        }
        return RedirectToAction("Index");
    }
}
