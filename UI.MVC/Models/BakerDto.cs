using System;
using System.ComponentModel.DataAnnotations;
using ProjectBakery.Domain;

namespace ProjectBakeryUI.MVC.Models
{
    public class BakerDto
    {
        //properties
        public int Id { get; set; }
        public Bakery Bakery { get;set; }
        [Required]
        [MinLength(3, ErrorMessage = "Name must be minimum 3 characters long!")]
        public string Name { get;set;}
        [Required]
        public DateTime BirthDate { get; set;}
        [Required]
        [Range(1500,10000, ErrorMessage = "Income must be between 1500 and 10000!")]
        public double Income { get;set; }
        
        public BakerDto(Baker b)
        {
            Id = b.Id;
            Name = b.Name;
            BirthDate = b.BirthDate;
            Income = b.Income;
            Bakery = b.Bakery;
        }

        public BakerDto()
        {
           
        }
    }
}