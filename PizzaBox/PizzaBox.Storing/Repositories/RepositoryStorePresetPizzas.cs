using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace PizzaBox.Storing.Repositories
{
    public class RepositoryStorePresetPizzas : IRepository<StorePresetPizzas>
    {
        PizzaDBContext db;
        public RepositoryStorePresetPizzas(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(StorePresetPizzas item)
        {
            //We need to see if the store id and order id exist
            if (db.StoreInfo.Any(e => e.StoreId == item.StoreId) && db.PresetPizzas.Any(e => e.PizzaName == item.PizzaName))
            {
                try
                {
                    db.StorePresetPizzas.Add(item);
                    db.SaveChanges();
                    Console.WriteLine("preset pizza addede succesfully");
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("Could not add to database");
                }
                
            }
            else
            {
                Console.WriteLine("StoreID or Pizza Name not found");
            }
           
        }

        public IEnumerable<StorePresetPizzas> GetItems()
        {
            var query = from e in db.StorePresetPizzas
                        select e;
            return query;
        }

        public void Modify(StorePresetPizzas item)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<StorePresetPizzas> GetStorePizzas(int id)
        {
            var query = from e in db.StorePresetPizzas
                        where e.StoreId == id
                        select e;
            return query;
        }
    }
}
