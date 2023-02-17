using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectBakery.DAL;
using ProjectBakery.Domain;
using Type = ProjectBakery.Domain.Type;


namespace ProjectBakery.BL
{
    public class Manager : IManager
    {
        //field
        private IRepository irep;


        //constructor
        public Manager(IRepository irep)
        {
            this.irep = irep;
        }


        //methods
        public Bakery GetBakery(int id)
        {
            return irep.ReadBakery(id);
        }

        public Pastrie GetPastrie(int id)
        {
            return irep.ReadPastrie(id);
        }

        public Baker GetBaker(int id)
        {
            return irep.ReadBaker(id);
        }

        public IEnumerable<Bakery> GetAllBakerys()
        {
            return irep.ReadAllBakerys();
        }

        public IEnumerable<Pastrie> GetAllPastries()
        {
            return irep.ReadAllPastries();
        }

        public IEnumerable<Baker> GetAllBakers()
        {
            return irep.ReadAllBakers();
        }

        public IEnumerable<Bakery> GetBakerysByNameAndLocation(string name, string location)
        {
            return irep.ReadBakerysByNameAndLocation(name, location);
        }

        public IEnumerable<Pastrie> GetPastriesByType(Type type)
        {
            return irep.ReadPastriesByType(type);
        }

        public Bakery AddBakery(string name, string location, int employees)
        {


            Bakery bakery = new Bakery()
                {Employees = employees, Location = location, Name = name};
            
            ValidationContext validationContext = new ValidationContext(bakery);
            Validator.ValidateObject(bakery, validationContext, validateAllProperties: true);
            irep.CreateBakery(bakery);
            return bakery;
        }

        public Pastrie AddPastrie( string name, double price, int quantity, Type type)
        {
            Pastrie pastrie = new Pastrie()
                {Name = name, Price = price, Quantity = quantity, Type = type};
            
            ValidationContext validationContext = new ValidationContext(pastrie);
            Validator.ValidateObject(pastrie, validationContext, validateAllProperties: true);
            
            Pastrie pastrie2=irep.CreatePastrie(pastrie);
            
            return pastrie2;
        }

        public Baker AddBaker(Bakery bakery, DateTime birthdate, double income, string name)
        {
            Baker baker = new Baker() {Bakery = bakery, BirthDate = birthdate,Income = income, Name = name};
            ValidationContext validationContext = new ValidationContext(baker);
            Validator.ValidateObject(baker, validationContext, validateAllProperties: true);
            irep.CreateBaker(baker);
            
            return baker;
        }

        public Pastrie GetAllBakeriesByPastrie(long id)
        {
            return irep.ReadAllBakeriesByPastrie(id);
        }

        public IEnumerable<Pastrie> GetAllPastriesByBakery(long id)
        {
            return irep.ReadAllPastriesByBakery(id);
        }

        public void AddPastryBakery(StockProduct newStockProduct)
        {
            irep.CreatePastryBakery(newStockProduct);
        }

        public void DeletePastryBakery(long bakeryId, long pastryId)
        {
            irep.DeletePastryBakery(bakeryId, pastryId);
        }

        public void ChangeBaker(Baker baker)
        {
            irep.UpdateBaker(baker);
        }
        
        public void ChangePastry(int id, int quantity)
        {
            irep.UpdatePastry(id, quantity);
        }

        public StockProduct AddStockProduct(long bakeryId, double price, double stock, long pastryId )
        {
            var stockP=irep.CreateStockProduct(bakeryId, price,stock, pastryId );
            return stockP;
        }
        
    }
}