using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Features.Subscribes.Commands.CreateSubscribe;

namespace NewsLetter.WebUI.Controllers;
//eger giriþ yapmadan hiç birþey yapýlmasýn istersek
//If we want nothing to be done without logging in
//[Authorize(AuthenticationSchemes ="Cookies")]
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
            TempData["status"] = "success";
        }
        else
        {
            TempData["Message"] = response.ErrorMessages!.First();
            TempData["status"] = "error";
        }
        return RedirectToAction("Index");
    }
}
