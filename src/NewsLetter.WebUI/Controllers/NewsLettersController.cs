using Microsoft.AspNetCore.Mvc;

namespace NewsLetter.WebUI.Controllers;
public class NewsLettersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
