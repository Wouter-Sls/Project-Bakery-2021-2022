using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectBakery.BL;
using ProjectBakery.Domain;
using ProjectBakeryUI.MVC.Models;

namespace ProjectBakeryUI.MVC.Controllers.Api
{
    [ApiController]
    [Route("/Api/[controller]")]
    public class BakersController : ControllerBase
    {
       
        private readonly IManager _manager;

        public BakersController(IManager manager)
        {

            _manager = manager;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Baker>> GetAll()
        {
            var bakers = _manager.GetAllBakers();
            if (bakers == null || !bakers.Any())
            {
                return NoContent();
            }

            return Ok(bakers);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Baker baker = _manager.GetBaker(id);
            if (baker == null)
            {
                return NotFound();
            }

            return Ok(baker);
        }

        [HttpPost]
        public ActionResult<Baker> PostBaker( [FromBody] Baker newBaker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // returns '400 Bad Request' with error-data
            }
        
            Baker baker = _manager.AddBaker(null, newBaker.BirthDate, newBaker.Income, newBaker.Name);
            return CreatedAtAction("GetById", new {id=baker.Id}, baker);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Baker baker)
        {

            if (id != baker.Id)
            {
                return Conflict(); // returns '400 Bad Request' with error-data
            }

            /*
            BakerDto bakerDto = new BakerDto();

            bakerDto.Id = baker.Id;
            bakerDto.Income = baker.Income;
            bakerDto.Name = baker.Name;
            bakerDto.BirthDate = baker.BirthDate;
            bakerDto.Bakery = baker.Bakery;

            
            ValidationContext validationContext = new ValidationContext(bakerDto);
            try
            {
                Validator.ValidateObject(bakerDto, validationContext, validateAllProperties: true);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }

            */
            
            if (baker.Name.Length <= 3)
            {
                return BadRequest("Name must be minimum 3 characters long!");
            }

            if (baker.Income >= 10000 || baker.Income <= 1500)
            {
                return BadRequest("Income must be between 1500 and 10000!");
            }
            
            
            
            _manager.ChangeBaker(baker);
            return NoContent();
        }




    }
}