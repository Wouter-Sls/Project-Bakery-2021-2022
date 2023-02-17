using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class BakerController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}