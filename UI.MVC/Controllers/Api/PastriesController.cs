using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectBakery.BL;

namespace ProjectBakeryUI.MVC.Controllers.Api
{
    [ApiController]
    [Route("/Api/[controller]")]
    public class PastriesController : ControllerBase
    {
        private readonly IManager _imanager;

        public PastriesController(IManager imanager)
        {
            _imanager = imanager;
        }

        
        //if statement met NoContent bijgevoegd
        [HttpGet]
        public IActionResult Get()
        {

            var pastries = _imanager.GetAllPastries();

            if (pastries == null || !pastries.Any())
            {
                return NoContent();
            }
            
            return Ok(pastries);
        }
    }
}