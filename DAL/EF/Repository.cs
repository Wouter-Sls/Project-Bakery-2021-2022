using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectBakery.Domain;

namespace ProjectBakery.DAL.EF
{
    public class Repository : IRepository
    {
        private ProjectBakeryDbContext dbContext;


        public Repository()
        {
            dbContext = new ProjectBakeryDbContext();
        }


        public Bakery ReadBakery(int id)
        {
            return dbContext.Bakeries.Find(id);
        }

        public Pastrie ReadPastrie(int id)
        {
            return dbContext.Pastries.Find(id);
        }

        public Baker ReadBaker(int id)
        {
            return dbContext.Bakers.Find(id);
        }

        public IEnumerable<Bakery> ReadAllBakerys()
        {
            return dbContext.Bakeries;
        }

        public IEnumerable<Pastrie> ReadAllPastries()
        {
            return dbContext.Pastries;
        }

        public IEnumerable<Baker> ReadAllBakers()
        {
            return dbContext.Bakers;
        }

        
        public IEnumerable<Bakery> ReadBakerysByNameAndLocation(string name, string location)
        {
            IQueryable<Bakery> bakeriesQueryable = dbContext.Bakeries;
            if (!string.IsNullOrEmpty(name))
            {
                bakeriesQueryable =  bakeriesQueryable.Where(bakery =>
                    bakery.Name.ToLower().Contains(name.ToLower()))
                    .Include(b=>b.Baker)
                    .Include(b=>b.StockProducts);; 
            }
            
            if (!string.IsNullOrEmpty(location))
            {
                bakeriesQueryable =  bakeriesQueryable.Where(bakery =>
                    bakery.Location.ToLower().Contains(location.ToLower()))
                    .Include(b=>b.Baker)
                    .Include(b=>b.StockProducts);
            }

            return bakeriesQueryable.AsEnumerable();
        }

        public IEnumerable<Pastrie> ReadPastriesByType(Type type)
        {
            return dbContext.Pastries.Where(pastrie => pastrie.Type == type);
        }

        public void CreateBakery(Bakery bakery)
        {
            dbContext.Bakeries.Add(bakery);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
        }

        public Pastrie CreatePastrie(Pastrie pastrie)
        {
            dbContext.Pastries.Add(pastrie);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
            return pastrie;
        }

        public Baker CreateBaker(Baker baker)
        {
            dbContext.Bakers.Add(baker);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
            return baker;
        }

        public Pastrie ReadAllBakeriesByPastrie(long id)
        {
            return dbContext.Pastries
                .Include(p => p.StockProducts)
                .ThenInclude(s => s.Bakerie)
                .FirstOrDefault(pa => pa.Id == id);
        }
        
        
        public IEnumerable<Pastrie> ReadAllPastriesByBakery(long id)
        {
            ICollection<Pastrie> listPastry = new List<Pastrie>();
            IEnumerable<StockProduct> listStock = new List<StockProduct>();

             listStock = dbContext.Bakeries.Include(b => b.StockProducts)
                .ThenInclude(s => s.Pastrie).FirstOrDefault(b => b.Id == id).StockProducts.AsEnumerable();
             
             foreach (var stockProduct in listStock)
             {
                 listPastry.Add(stockProduct.Pastrie);
             }

             return listPastry;
        }

        public void CreatePastryBakery(StockProduct newStockProduct)
        {
            newStockProduct.Bakerie.StockProducts.Add(newStockProduct);
            newStockProduct.Pastrie.StockProducts.Add(newStockProduct);
            dbContext.StockProducts.Add(newStockProduct);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
        }

        public void DeletePastryBakery(long bakeryId, long pastryId)
        {
            Bakery bakery = dbContext.Bakeries.Find((int) bakeryId);

            bakery.StockProducts.Remove(bakery.StockProducts
                .ToArray().ElementAt((int) (pastryId - 1)));
        }

        public void UpdateBaker(Baker newBaker)
        {
            Baker baker = dbContext.Bakers.Find(newBaker.Id);
            
            baker.Name = newBaker.Name;
            baker.Income = newBaker.Income;
            
            dbContext.SaveChanges();
        }


        public void UpdatePastry(int id, int quantity)
        {
            Pastrie p = dbContext.Pastries.Find(id);

            p.Quantity += quantity;
            
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
        }

        public StockProduct CreateStockProduct(long bakeryId, double price, double stock, long pastryId)
        {
            Bakery bakery = ReadBakery((int) bakeryId);
            Pastrie pastry = ReadPastrie((int) pastryId);
            StockProduct stockP = new StockProduct(){Bakerie = bakery, Pastrie = pastry, TotalPrice = price, TotalStock = stock};
            
            dbContext.StockProducts.Add(stockP);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();

            return stockP;
        }
    }
}