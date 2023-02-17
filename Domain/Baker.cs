using System;

namespace ProjectBakery.Domain
{
    public class Baker
    {
        //properties
        public int Id { get; set; }
        public Bakery Bakery { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public double Income { get; set; }
    }
}