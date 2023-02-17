using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjectBakery.Domain
{
    //enum
    public enum Type
    {
        Bread,
        Cake,
        Cookies
    }

    public class Pastrie : IValidatableObject
    {
        //properties
        public int Id { get; set; }
        [Required] [StringLength(30, MinimumLength = 3)] public string Name { get; set; }
        [Range(1,15)] public double Price { get; set; }
        public int Quantity { get; set; }
        public Type Type { get; set; }
        public ICollection<StockProduct> StockProducts { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Name.Any(char.IsDigit))
            {
                string errorMessage = "Error: Digits cannot be used in name";
                errors.Add(new ValidationResult(errorMessage, new[]
                {
                    "Name"
                }));
            }

            return errors;
        }
    }
}