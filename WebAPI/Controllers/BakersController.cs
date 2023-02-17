using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class BakersController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}