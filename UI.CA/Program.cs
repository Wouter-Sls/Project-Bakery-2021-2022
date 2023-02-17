using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectBakery.BL;
using ProjectBakery.DAL.EF;
using ProjectBakery.Domain;
using ProjectBakery.UI.CA.Extensions;
using Type = ProjectBakery.Domain.Type;

namespace ProjectBakery.UI.CA
{
    class Program
    {
        private IManager iManager;

        public Program(Repository repository)
        {
            iManager = new Manager(repository);
        }

        static void Main(string[] args)
        {
            Repository repository = new Repository();
            Program program = new Program(repository);
            program.Run();
        }

        public void Run()
        {
            //fields
            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("=========================");
                Console.WriteLine("0) Quit");
                Console.WriteLine("1) Show all pastries");
                Console.WriteLine("2) Show all bakery's");
                Console.WriteLine("3) Show pastries by type");
                Console.WriteLine("4) Show bakery's with name and/or location");
                Console.WriteLine("5) Add a pastry");
                Console.WriteLine("6) Add a bakery");
                Console.WriteLine("7) Add pastry to bakery");
                Console.WriteLine("8) Remove pastry from bakery");
                Console.Write("Choice 0-8:");
                choice = Convert.ToInt16(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ShowAllPastries();
                        break;
                    case 2:
                        ShowAllBakeries();
                        break;
                    case 3:
                        GetPastryByType();
                        break;
                    case 4:
                        GetBakeryByPartNamePartLocation();
                        break;
                    case 5:
                        AddPastrie();
                        break;
                    case 6:
                        AddBakery();
                        break;
                    case 7:
                        AddPastryToBakery();
                        break;
                    case 8:
                        RemovePastryFromBakery();
                        break;
                }
            }
        }
        
        
        
        private void ShowAllPastries()
        {
            Console.WriteLine("All pastries");
            Console.WriteLine("============");
            foreach (var pastr in iManager.GetAllPastries())
            {
                Console.WriteLine(pastr.GetInfo());
            }
        }
        private void ShowAllBakeries()
        {
            Console.WriteLine("All Bakery's");
            Console.WriteLine("============");

            int count = 0;

            foreach (var bkr in iManager.GetAllBakers())
            {
                count++;
                Console.WriteLine("\n" + bkr.GetInfo());
                foreach (var pas in iManager.GetAllPastriesByBakery(count))
                {
                    Console.WriteLine(pas.Name);
                }
            }
        }
        private void GetPastryByType()
        {
            Console.Write("Types (1=Bread, 2=Cake, 3=Cookie):");
            int typeChoice = Convert.ToInt16(Console.ReadLine());

            switch (typeChoice)
            {
                case 1:

                    foreach (var pstrT in iManager.GetPastriesByType(Type.Bread))
                    {
                        Console.WriteLine(pstrT.GetInfo());
                    }

                    break;
                case 2:

                    foreach (var pstrT in iManager.GetPastriesByType(Type.Cake))
                    {
                        Console.WriteLine(pstrT.GetInfo());
                    }

                    break;
                case 3:
                    foreach (var pstrT in iManager.GetPastriesByType(Type.Cookies))
                    {
                        Console.WriteLine(pstrT.GetInfo());
                    }

                    break;
            }
        }
        private void GetBakeryByPartNamePartLocation()
        {
            Console.WriteLine();
            Console.Write("Enter (part of) a name or leave blank: ");
            String partName = Console.ReadLine();

            Console.Write("Enter (part of) a location or leave blank: ");
            String partOfLocation = Console.ReadLine();

            foreach (var bkryNl in iManager.GetBakerysByNameAndLocation(partName, partOfLocation))
            {
                Console.WriteLine(bkryNl.GetInfo());
            }
        }

        private void AddPastrie()
        {
            String namePastrie = "";
            double price = 0;
            int quantity = 0;
            int type = 0;
            Type pastrieType = new Type();

            Console.WriteLine();
            Console.WriteLine("Add pastrie");
            Console.WriteLine("===========");
            Console.Write("Name:");
            namePastrie = Console.ReadLine();
            Console.Write("Price:");
            price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Quantity:");
            quantity = Convert.ToInt16(Console.ReadLine());
            Console.Write("Type (1=Bread, 2=Cake, 3=Cookies):");
            type = Convert.ToInt16(Console.ReadLine());

            switch (type)
            {
                case 1:
                    pastrieType = Type.Bread;
                    break;
                case 2:
                    pastrieType = Type.Cake;
                    break;
                case 3:
                    pastrieType = Type.Cookies;
                    break;
            }

            try
            {
                iManager.AddPastrie(namePastrie, price, quantity, pastrieType);
            }
            catch (ValidationException e)
            {
                Console.WriteLine("\n" + e.Message);
                AddPastrie();
            }
        }
        
        private void AddBakery()
        {
            Console.WriteLine();
            Console.WriteLine("Add bakery");
            Console.WriteLine("===========");
            Console.Write("Name:");
            String nameBakery = Console.ReadLine();
            Console.Write("Location:");
            String location = Console.ReadLine();
            Console.Write("Employees:");
            int employees = Convert.ToInt32(Console.ReadLine());

            try
            {
                iManager.AddBakery(nameBakery, location, employees);
            }
            catch (ValidationException e)
            {
                Console.WriteLine("\n" + e.Message);
                AddBakery();
            }
        }
        private void AddPastryToBakery()
        {
            Console.WriteLine("Which bakery would you like to add a pastry to?");
            foreach (var bakery in iManager.GetAllBakerys())
            {
                Console.WriteLine("[{0}] {1}", bakery.Id, bakery.Name);
            }

            Console.Write("Please enter a bakery ID: ");
            int bakeryId = int.Parse(Console.ReadLine());

            Console.WriteLine("Which pastry would you like to assign to this bakery?");
            foreach (var pastry in iManager.GetAllPastries())
            {
                Console.WriteLine("[{0}] {1}", pastry.Id, pastry.Name);
            }

            Console.Write("Please enter a pastry ID: ");
            int pastryId = int.Parse(Console.ReadLine());

            Bakery baker = iManager.GetBakery(bakeryId);
            Pastrie pastrie = iManager.GetPastrie(pastryId);

            StockProduct newStock = new StockProduct()
                {Bakerie = baker, Pastrie = pastrie, TotalPrice = 127.5, TotalStock = 15};
            iManager.AddPastryBakery(newStock);
        }
        
        private void RemovePastryFromBakery()
        {
            Console.WriteLine("Which bakery would you like to remove a pastry from?");
            foreach (var bakery in iManager.GetAllBakerys())
            {
                Console.WriteLine("[{0}] {1}", bakery.Id, bakery.Name);
            }

            Console.Write("Please enter a bakery ID: ");
            int bId = int.Parse(Console.ReadLine());

            Console.WriteLine("Which pastry would you like to remove from this bakery?");

            ICollection<StockProduct> chosenBaker = iManager.GetBakery(bId).StockProducts;

            for (int i = 0; i < chosenBaker.Count; i++)
            {
                Console.WriteLine("[{0}] {1}", i + 1, chosenBaker.ToArray().ElementAt(i).Pastrie.Name);
            }

            Console.Write("Please enter a pastry ID: ");
            int pId = int.Parse(Console.ReadLine());

            iManager.DeletePastryBakery(bId, pId);
        }
    }
}