using System;
using System.Collections.Generic;
using ProjectBakery.Domain;
using Type = ProjectBakery.Domain.Type;

namespace ProjectBakery.BL
{
    public interface IManager
    {
        public Bakery GetBakery(int id);

        public Pastrie GetPastrie(int id);

        public Baker GetBaker(int id);
        
        public IEnumerable<Bakery> GetAllBakerys();
        
        public IEnumerable<Pastrie> GetAllPastries();
        
        public IEnumerable<Baker> GetAllBakers();
        
        public IEnumerable<Bakery> GetBakerysByNameAndLocation(string name, string location);

        public IEnumerable<Pastrie> GetPastriesByType(Type type);

        public Bakery AddBakery(string name,string location, int employees);

        public Pastrie AddPastrie( string name, double price, int quantity, Type type);

        public Baker AddBaker(Bakery bakery,DateTime birthdate, double income, string name);
        public Pastrie GetAllBakeriesByPastrie(long id);
        public IEnumerable<Pastrie> GetAllPastriesByBakery(long id);
        public void AddPastryBakery(StockProduct newStockProduct);
        public void DeletePastryBakery(long bakeryId, long pastryId);
        public void ChangeBaker(Baker baker);
        public void ChangePastry(int id, int quantity);
        public StockProduct AddStockProduct(long bakeryId, double price, double stock, long pastryId );
    }
}