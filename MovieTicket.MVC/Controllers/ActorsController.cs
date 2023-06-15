using Microsoft.AspNetCore.Mvc;

namespace MovieTicket.MVC.Controllers;

public class ActorsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
