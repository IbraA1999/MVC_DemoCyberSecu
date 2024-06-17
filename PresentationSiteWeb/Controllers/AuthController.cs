using Microsoft.AspNetCore.Mvc;

namespace PresentationSiteWeb.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult  Register() 
        { 
            return View();
        }
    }
}
