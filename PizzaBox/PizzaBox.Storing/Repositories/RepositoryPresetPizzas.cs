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
    public class RepositoryPresetPizzas : IRepository<PresetPizzas>
    {
        PizzaDBContext db;
        public RepositoryPresetPizzas(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(PresetPizzas item)
        {
            if (db.PresetPizzas.Any(e => e.PizzaName == item.PizzaName))
            {
                Console.WriteLine("Pizza with this name already exists");
            }
            else
            {
                db.PresetPizzas.Add(item);
                db.SaveChanges();
                Console.WriteLine("Pizza craeted successfully");
               
                
            }
            
        }

        public IEnumerable<PresetPizzas> GetItems()
        {
            var query = from e in db.PresetPizzas
                        select e;

            return query;
        }

        public void Modify(PresetPizzas item)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public PresetPizzas GetPizza(string name)
        {
            foreach(PresetPizzas ps in db.PresetPizzas)
            {
                if (ps.PizzaName == name)
                {
                    return ps;
                }
            }
            return null;
        }
    }
}
