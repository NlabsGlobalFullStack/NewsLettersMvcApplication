using Azure;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.Features.Auth.Login;
using System.Security.Claims;

namespace NewsLetter.WebUI.Controllers;
public class AuthController(IMediator mediator) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        TempData["UserNameOrEmail"] = request.UserNameOrEmail;
        TempData["Password"] = request.Password;

        if (response.StatusCode == 200)
        {
            TempData["Message"] = response.Data;
            TempData["status"] = "success";
        }
        else if(response.StatusCode == 500)
        {
            TempData["Message"] = response.ErrorMessages!.First();
            TempData["status"] = "error";
        }

        if (!response.IsSuccessful)
        {
            return RedirectToAction("Login");
        }

        List<Claim> claims = new()
        {
            new Claim("Name", "Cuma KÖSE")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return RedirectToAction("Index", "Home");
    }

    
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync();
        TempData["Message"] = "Logout successful!";
        TempData["status"] = "info";
        return RedirectToAction("Login");
    }
}
