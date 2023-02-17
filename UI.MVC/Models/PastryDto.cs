using ProjectBakery.Domain;

namespace ProjectBakeryUI.MVC.Models
{
    public class PastryDto
    {
        //properties
        public int Id { get;}
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; }
        public Type Type { get; }

        public PastryDto(Pastrie p)
        {
            Id = p.Id;
            Name = p.Name;
            Price = p.Price;
            Quantity = p.Quantity;
            Type = p.Type;
        }
    }
    
   
}