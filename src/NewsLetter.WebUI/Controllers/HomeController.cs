using Microsoft.AspNetCore.Mvc;

namespace NewsLetter.WebUI.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
