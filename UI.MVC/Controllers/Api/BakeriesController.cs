using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectBakery.BL;
using ProjectBakery.Domain;
using ProjectBakeryUI.MVC.Models;

namespace ProjectBakeryUI.MVC.Controllers.Api
{
    
    [ApiController]
    [Route("/Api/[controller]")]
    public class BakeriesController : ControllerBase
    {
        
        private readonly IManager _imanager;

        public BakeriesController(IManager imanager)
        {
            _imanager = imanager;
        }
        
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Pastrie>> GetAll(int id)
        {
            var list = new List<PastryDto>();
            var pastries = _imanager.GetAllPastriesByBakery(id);

            if (pastries == null || !pastries.Any())
            {
                return NoContent();
            }

            foreach (var newP in pastries)
            {
                foreach (var stockProduct in newP.StockProducts)
                {
                    if (stockProduct.Bakerie.Id==id)
                    {
                        newP.Quantity = (int) stockProduct.TotalStock;
                    }
                }
                
                list.Add(new PastryDto(newP));
            }
            return Ok(list);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] StockProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // returns '400 Bad Request' with error-data
            }

            _imanager.ChangePastry((int) dto.PastryId, (int)dto.TotalStock);

            double totalPrice = dto.TotalStock * _imanager.GetPastrie((int) dto.PastryId).Price;
            StockProduct stockProduct = _imanager.AddStockProduct(dto.BakeryId, totalPrice, dto.TotalStock, dto.PastryId);
            
            return Ok(stockProduct);
        }
    }
}