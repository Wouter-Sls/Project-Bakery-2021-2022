using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectBakery.Domain
{
    public class Bakery
    {
        //properties
        public int Id { get; set; }
        [Required] [StringLength(30,MinimumLength = 5)] public string Name{ get; set; } 
        public string Location{ get; set; }
        public int Employees { get; set; }
        public ICollection<StockProduct> StockProducts{ get; set; }
        [Required]
        public Baker Baker { get; set; }
    }
}