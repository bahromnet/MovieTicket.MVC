using Microsoft.AspNetCore.Mvc;

namespace MovieTicket.MVC.Controllers
{
    public class CinemasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
