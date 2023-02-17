using System;
using System.Collections.Generic;
using ProjectBakery.Domain;
using Type = ProjectBakery.Domain.Type;

namespace ProjectBakery.DAL.EF
{
    public static class ProjectBakeryInitializer
    {
        private static bool _isInitialized;

        public static void Initialize(ProjectBakeryDbContext dbContext, bool isRebuild)
        {
            if (!_isInitialized)
            {
                if (isRebuild)
                {
                    dbContext.Database.EnsureDeleted();
                }


                if (dbContext.Database.EnsureCreated())
                {
                    Seed(dbContext);
                }
                _isInitialized = true;
            }
        }


        public static void Seed(ProjectBakeryDbContext dbContext)
        {

            //creating pastries & bakeries
            Pastrie wholegrain = new Pastrie() { Name = "Whole grain bread", Price = 2.99, Type = Type.Bread}; //Q=16
            Pastrie chocolateCake = new Pastrie() {Name = "Chocolate cake", Price = 7.50, Type = Type.Cake}; //Q=12
            Pastrie chocolateCookies = new Pastrie() {Name = "Chocolate cookies", Price = 1.50, Type = Type.Cookies};//Q=24
            Pastrie strawberryCake = new Pastrie() {Name = "Strawberry cake", Price = 8.50, Type = Type.Cake};//Q=9
            
            Bakery bakkerAldo = new Bakery() {Name = "Bakker Aldo", Location = "Lange Leemstraat 388, 2018 Antwerpen", Employees = 6};
            Bakery vervecken = new Bakery() {Name = "Bakkerij Vervecken", Location = "Volkstraat 12, 2000 Antwerpen", Employees = 4};
            Bakery theBakery = new Bakery() {Name = "The Bakery", Location = "Boomgaardstraat 1, 2018 Antwerpen", Employees = 5};
            Bakery goossens = new Bakery() {Name = "Goossens", Location = "Korte Gasthuisstraat 31, 2000 Antwerpen", Employees = 4};

            Baker aldo = new Baker() {Bakery = bakkerAldo, BirthDate = new DateTime(1985, 10, 8), Income = 2300, Name = "Aldo"};
            Baker verv = new Baker() {Bakery = vervecken, BirthDate = new DateTime(1996, 1, 20), Income = 1900, Name = "Vervecken"};
            Baker jan = new Baker() {Bakery = theBakery, BirthDate = new DateTime(1974, 5, 12), Income = 2400, Name = "Jan"};
            Baker goos = new Baker() {Bakery = goossens, BirthDate = new DateTime(1981, 11, 4), Income = 1800, Name = "Goossens"};

            bakkerAldo.Baker = aldo;
            vervecken.Baker = verv;
            theBakery.Baker = jan;
            goossens.Baker = goos;

            StockProduct stockAldoWhole = new StockProduct(){Bakerie = bakkerAldo, Pastrie = wholegrain, TotalPrice = 89.7, TotalStock = 30};
            StockProduct stockAldoChoCake = new StockProduct(){Bakerie = bakkerAldo, Pastrie = chocolateCake, TotalPrice = 150, TotalStock = 20};
            StockProduct stockAldoStrawCake = new StockProduct(){Bakerie = bakkerAldo, Pastrie = strawberryCake, TotalPrice = 127.5, TotalStock = 15};
            
            StockProduct stockVervWhole = new StockProduct(){Bakerie = vervecken, Pastrie = wholegrain, TotalPrice = 44.85, TotalStock = 15};
            StockProduct stockVervCookies = new StockProduct(){Bakerie = vervecken, Pastrie = chocolateCookies, TotalPrice = 37.5, TotalStock = 25};
            StockProduct stockVervStrawCake = new StockProduct(){Bakerie = vervecken, Pastrie = strawberryCake, TotalPrice = 153, TotalStock = 18};
            
            StockProduct stockTheBakeryWhole = new StockProduct(){Bakerie = theBakery, Pastrie = wholegrain, TotalPrice = 59.8, TotalStock = 20};
            StockProduct stockTheBakeryChoCake = new StockProduct(){Bakerie = theBakery,Pastrie = chocolateCake, TotalPrice = 112.5, TotalStock = 15};
            StockProduct stockTheBakeryCookies = new StockProduct(){Bakerie = theBakery, Pastrie = chocolateCookies, TotalPrice = 45, TotalStock = 30};
            
            StockProduct stockGoWhole = new StockProduct(){Bakerie = goossens, Pastrie = wholegrain, TotalPrice = 35.88, TotalStock = 12};
            StockProduct stockGoChoCake = new StockProduct(){Bakerie = goossens, Pastrie = chocolateCake, TotalPrice = 75, TotalStock = 10};
            StockProduct stockGoStrawCake = new StockProduct(){Bakerie = goossens, Pastrie = strawberryCake, TotalPrice = 187, TotalStock = 22};

            wholegrain.Quantity = (int) stockAldoWhole.TotalStock + (int) stockVervWhole.TotalStock + (int) stockTheBakeryWhole.TotalStock + (int) stockGoWhole.TotalStock;
            chocolateCake.Quantity = (int) stockAldoChoCake.TotalStock + (int) stockTheBakeryChoCake.TotalStock + (int) stockGoChoCake.TotalStock;
            chocolateCookies.Quantity = (int) stockVervCookies.TotalStock + (int) stockTheBakeryCookies.TotalStock;
            strawberryCake.Quantity = (int) stockAldoStrawCake.TotalStock + (int) stockVervStrawCake.TotalStock + (int) stockGoStrawCake.TotalStock;

            bakkerAldo.StockProducts = new List<StockProduct>() {stockAldoWhole, stockAldoChoCake, stockAldoStrawCake};
            vervecken.StockProducts = new List<StockProduct>() {stockVervWhole, stockVervCookies, stockVervStrawCake};
            theBakery.StockProducts = new List<StockProduct>() {stockTheBakeryWhole, stockTheBakeryChoCake, stockTheBakeryCookies};
            goossens.StockProducts = new List<StockProduct>() {stockGoWhole, stockGoChoCake, stockGoStrawCake};

            wholegrain.StockProducts = new List<StockProduct>() {stockAldoWhole, stockVervWhole, stockTheBakeryWhole};
            chocolateCake.StockProducts = new List<StockProduct>() {stockTheBakeryChoCake, stockGoChoCake, stockAldoChoCake};
            chocolateCookies.StockProducts = new List<StockProduct>() {stockVervCookies, stockVervCookies, stockTheBakeryCookies};
            strawberryCake.StockProducts = new List<StockProduct>() {stockVervStrawCake, stockGoStrawCake, stockAldoStrawCake};


            //add to database
            dbContext.Bakeries.Add(bakkerAldo);
            dbContext.Bakeries.Add(vervecken);
            dbContext.Bakeries.Add(theBakery);
            dbContext.Bakeries.Add(goossens);

            dbContext.Pastries.Add(wholegrain);
            dbContext.Pastries.Add(chocolateCake);
            dbContext.Pastries.Add(chocolateCookies);
            dbContext.Pastries.Add(strawberryCake);
            
            dbContext.Bakers.Add(aldo);
            dbContext.Bakers.Add(verv);
            dbContext.Bakers.Add(jan);
            dbContext.Bakers.Add(goos);

            dbContext.StockProducts.Add(stockAldoWhole);
            dbContext.StockProducts.Add(stockGoWhole);
            dbContext.StockProducts.Add(stockVervCookies);
            dbContext.StockProducts.Add(stockVervWhole);
            dbContext.StockProducts.Add(stockAldoChoCake);
            dbContext.StockProducts.Add(stockAldoStrawCake);
            dbContext.StockProducts.Add(stockGoChoCake);
            dbContext.StockProducts.Add(stockGoStrawCake);
            dbContext.StockProducts.Add(stockTheBakeryCookies);
            dbContext.StockProducts.Add(stockTheBakeryWhole);
            dbContext.StockProducts.Add(stockVervStrawCake);
            dbContext.StockProducts.Add(stockTheBakeryChoCake);

            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
        }
    }
}