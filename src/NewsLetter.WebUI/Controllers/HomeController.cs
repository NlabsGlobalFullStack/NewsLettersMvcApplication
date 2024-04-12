using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Features.Subscribes.Commands.CreateSubscribe;
using NewsLetter.WebUI.Helpers;

namespace NewsLetter.WebUI.Controllers;
//eger giriþ yapmadan hiç birþey yapýlmasýn istersek
//If we want nothing to be done without logging in
//[Authorize(AuthenticationSchemes ="Cookies")]
public class HomeController : BaseController
{
    public HomeController(IMediator mediator) : base(mediator) {}

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(string email)
    {
        var response = await _mediator.Send(new CreateSubscribeCommand(email));

        MyHelper.Handle(this, response, out result);

        if (result == 200)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Index");
        }
    }
}
