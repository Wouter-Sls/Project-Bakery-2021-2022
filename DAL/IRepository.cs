using System;
using System.Collections.Generic;
using ProjectBakery.Domain;
using Type = ProjectBakery.Domain.Type;


namespace ProjectBakery.DAL
{
    public interface IRepository
    {
        public Bakery ReadBakery(int id);
        public Pastrie ReadPastrie(int id);
        public Baker ReadBaker(int id);
        public IEnumerable<Bakery> ReadAllBakerys();
        public IEnumerable<Pastrie> ReadAllPastries();
        public IEnumerable<Baker> ReadAllBakers();
        public IEnumerable<Bakery> ReadBakerysByNameAndLocation(string name, string location);
        public IEnumerable<Pastrie> ReadPastriesByType(Type type);
        public void CreateBakery(Bakery bakery);
        public Pastrie CreatePastrie(Pastrie pastrie);
        public Baker CreateBaker(Baker baker);
        public Pastrie ReadAllBakeriesByPastrie(long id);
        public void CreatePastryBakery(StockProduct newStockProduct);
        public void DeletePastryBakery(long bakeryId, long pastryId);
        public void UpdateBaker(Baker baker);
        public void UpdatePastry(int id, int quantity);
        public IEnumerable<Pastrie> ReadAllPastriesByBakery(long id);
        public StockProduct CreateStockProduct(long bakeryId, double price, double stock, long pastryId);
    }
}