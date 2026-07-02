using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymRoute.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
