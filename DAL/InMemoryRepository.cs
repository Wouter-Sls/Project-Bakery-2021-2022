using System;
using System.Collections.Generic;
using ProjectBakery.Domain;
using Type = ProjectBakery.Domain.Type;

namespace ProjectBakery.DAL
{
    public class InMemoryRepository : IRepository
    {
        public static List<Bakery> listBakery = new List<Bakery>();
        public static List<Pastrie> listPastrie = new List<Pastrie>();

        public InMemoryRepository()
        {
            Seed();
        }

        public void Seed()
        {

            Pastrie wholegrain;
            Pastrie chocolateCake;
            Pastrie chocolateCookies;
            Pastrie strawberryCake;
            
            
            Bakery bakkerAldo;
            Bakery vervecken;
            Bakery theBakery;
            Bakery goossens;
            
            
            CreateBakery(bakkerAldo= new Bakery()
            {
                Name = "Bakker Aldo", Location = "Lange Leemstraat 388, 2018 Antwerpen", Employees = 6
            });
            CreateBakery(vervecken=new Bakery()
            {
                Name = "Bakkerij Vervecken", Location = "Volkstraat 12, 2000 Antwerpen", Employees = 4
            });
            CreateBakery(theBakery=new Bakery()
            {
                Name = "The Bakery", Location = "Boomgaardstraat 1, 2018 Antwerpen", Employees = 5
            });
            CreateBakery(goossens=new Bakery()
            {
                Name = "Goossens", Location = "Korte Gasthuisstraat 31, 2000 Antwerpen", Employees = 4
            });

            CreatePastrie(wholegrain=new Pastrie()
            {
                Name = "Whole grain bread", Price = 2.99, Quantity = 16, Type = Type.Bread
            });
            CreatePastrie(chocolateCake=new Pastrie()
            {
                Name = "Chocolate cake", Price = 7.50, Quantity = 12, Type = Type.Cake
            });
            CreatePastrie(chocolateCookies=new Pastrie()
            {
                Name = "Chocolate cookies", Price = 1.50, Quantity = 24, Type = Type.Cookies
            });
            CreatePastrie(strawberryCake=new Pastrie()
            {
                Name = "Strawberry cake", Price = 8.50, Quantity = 9, Type = Type.Cake
            });
            
            
            StockProduct stockAldoWhole = new StockProduct() {Bakerie = bakkerAldo, Pastrie = wholegrain, TotalPrice = 89.7, TotalStock = 30};
            StockProduct stockAldoChoCake = new StockProduct() {Bakerie = bakkerAldo, Pastrie = chocolateCake, TotalPrice = 150, TotalStock = 20};
            StockProduct stockAldoCookies = new StockProduct() {Bakerie = bakkerAldo, Pastrie = chocolateCookies, TotalPrice = 37.5, TotalStock = 25};
            StockProduct stockAldoStrawCake = new StockProduct() {Bakerie = bakkerAldo, Pastrie = strawberryCake, TotalPrice = 127.5, TotalStock = 15};

            StockProduct stockVervWhole = new StockProduct() {Bakerie = vervecken, Pastrie = wholegrain, TotalPrice = 44.85, TotalStock = 15};
            StockProduct stockVervChoCake = new StockProduct() {Bakerie = vervecken, Pastrie = chocolateCake, TotalPrice = 210, TotalStock = 28};
            StockProduct stockVervCookies = new StockProduct() {Bakerie = vervecken, Pastrie = chocolateCookies, TotalPrice = 37.5, TotalStock = 25};
            StockProduct stockVervStrawCake = new StockProduct() {Bakerie = vervecken, Pastrie = strawberryCake, TotalPrice = 153, TotalStock = 18};

            StockProduct stockTheBakeryWhole = new StockProduct() {Bakerie = theBakery, Pastrie = wholegrain, TotalPrice = 59.8, TotalStock = 20};
            StockProduct stockTheBakeryChoCake = new StockProduct() {Bakerie = theBakery, Pastrie = chocolateCake, TotalPrice = 112.5, TotalStock = 15};
            StockProduct stockTheBakeryCookies = new StockProduct() {Bakerie = theBakery, Pastrie = chocolateCookies, TotalPrice = 45, TotalStock = 30};
            StockProduct stockTheBakeryStrawCake = new StockProduct() {Bakerie = theBakery, Pastrie = strawberryCake, TotalPrice = 85, TotalStock = 10};

            StockProduct stockGoWhole = new StockProduct() {Bakerie = goossens, Pastrie = wholegrain, TotalPrice = 35.88, TotalStock = 12};
            StockProduct stockGoChoCake = new StockProduct() {Bakerie = goossens, Pastrie = chocolateCake, TotalPrice = 75, TotalStock = 10};
            StockProduct stockGoCookies = new StockProduct() {Bakerie = goossens, Pastrie = chocolateCookies, TotalPrice = 30, TotalStock = 20};
            StockProduct stockGoStrawCake = new StockProduct() {Bakerie = goossens, Pastrie = strawberryCake, TotalPrice = 187, TotalStock = 22};


            bakkerAldo.StockProducts = new List<StockProduct>() {stockAldoWhole, stockAldoChoCake, stockAldoStrawCake};
            vervecken.StockProducts = new List<StockProduct>() {stockVervWhole, stockVervCookies, stockVervStrawCake};
            theBakery.StockProducts = new List<StockProduct>() {stockTheBakeryWhole, stockTheBakeryChoCake, stockTheBakeryCookies};
            goossens.StockProducts = new List<StockProduct>() {stockGoWhole, stockGoChoCake, stockGoStrawCake};

            wholegrain.StockProducts = new List<StockProduct>() {stockAldoWhole, stockVervWhole, stockTheBakeryWhole};
            chocolateCake.StockProducts = new List<StockProduct>() {stockVervChoCake, stockTheBakeryChoCake, stockGoChoCake};
            chocolateCookies.StockProducts = new List<StockProduct>() {stockAldoCookies, stockVervCookies, stockGoCookies };
            strawberryCake.StockProducts = new List<StockProduct>() {stockVervStrawCake, stockTheBakeryStrawCake, stockGoStrawCake};

        }

        public Bakery ReadBakery(int id)
        {
            foreach (Bakery bakery in listBakery)
            {
                if (bakery.Id == id)
                {
                    return bakery;
                }
            }


            return null;
        }

        public Pastrie ReadPastrie(int id)
        {
            foreach (Pastrie pastrie in listPastrie)
            {
                if (pastrie.Id == id)
                {
                    return pastrie;
                }
            }

            return null;
        }

        public Baker ReadBaker(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bakery> ReadAllBakerys()
        {
            return listBakery;
        }

        public IEnumerable<Pastrie> ReadAllPastries()
        {
            return listPastrie;
        }

        public IEnumerable<Baker> ReadAllBakers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bakery> ReadBakerysByNameAndLocation(String name, String location)
        {
            List<Bakery> listPartBakery = new List<Bakery>();
            foreach (var bakery in listBakery)
            {
                if (bakery.Name.ToLower().Contains(name.ToLower()))
                {
                    if (bakery.Location.ToLower().Contains(location.ToLower()))
                    {
                        listPartBakery.Add(bakery);
                    }
                }
            }

            return listPartBakery;
        }

        public IEnumerable<Pastrie> ReadPastriesByType(Type type)
        {
            List<Pastrie> listPartPastries = new List<Pastrie>();
            foreach (var pastrie in listPastrie)
            {
                if (pastrie.Type.Equals(type))
                {
                    listPartPastries.Add(pastrie);
                }
            }

            return listPartPastries;
        }

        public void CreateBakery(Bakery bakery)
        {
            bakery.Id = listBakery.Count + 1;
            listBakery.Add(bakery);
        }

        public Pastrie CreatePastrie(Pastrie pastrie)
        {
            pastrie.Id = listPastrie.Count + 1;
            listPastrie.Add(pastrie);
            return pastrie;
        }

        public Baker CreateBaker(Baker baker)
        {
            throw new NotImplementedException();
        }

        public Pastrie ReadAllBakeriesByPastrie(long id)
        {
            throw new NotImplementedException();
        }

        public void CreatePastryBakery(StockProduct newStockProduct)
        {
            throw new NotImplementedException();
        }

        public void DeletePastryBakery(long bakeryId, long pastryId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBaker(Baker baker)
        {
            throw new NotImplementedException();
        }

        public void UpdatePastry(int id, int quantity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pastrie> ReadAllPastriesByBakery(long id)
        {
            throw new NotImplementedException();
        }

        public StockProduct CreateStockProduct(long bakeryId, double price, double stock, long pastryId)
        {
            throw new NotImplementedException();
        }
    }
}