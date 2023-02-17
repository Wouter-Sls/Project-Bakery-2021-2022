using Microsoft.AspNetCore.Mvc;
using ProjectBakery.BL;
using ProjectBakery.Domain;

namespace ProjectBakeryUI.MVC.Controllers
{
    public class PastryController : Controller
    {
        private readonly IManager _manager;

        public PastryController(IManager imanager)
        {
            _manager = imanager;
        }
        
        
        // GET
        public IActionResult Index()
        {
            return View(_manager.GetAllPastries());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Pastrie pastrie)
        {
            if (!ModelState.IsValid)
            {
                return View(pastrie);
            }

            Pastrie pas = _manager.AddPastrie(pastrie.Name, pastrie.Price, pastrie.Quantity, pastrie.Type);
            return RedirectToAction("Detail", new {id=pas.Id});
            

        }
        
        public IActionResult Detail(int id)
        {
            return View(_manager.GetAllBakeriesByPastrie(id));
        }
        
        public IActionResult DetailBakery(int id)
        {
            return View(_manager.GetBakery(id));
        }
        
        
        
        
    }
}