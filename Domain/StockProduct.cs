using System.ComponentModel.DataAnnotations;

namespace ProjectBakery.Domain
{
    public class StockProduct
    {
        public double TotalStock { get; set; }
        public double TotalPrice { get; set; }
        public int Id { get; set; }

        [Required]
        public Bakery Bakerie { get; set; }
        [Required]
        public Pastrie Pastrie { get; set; }

    }
}