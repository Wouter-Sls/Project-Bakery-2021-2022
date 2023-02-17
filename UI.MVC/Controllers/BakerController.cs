using Microsoft.AspNetCore.Mvc;

namespace ProjectBakeryUI.MVC.Controllers
{
    public class BakerController : Controller
    {

        // GET
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Detail()
        {
            return View();
        }
    }
}