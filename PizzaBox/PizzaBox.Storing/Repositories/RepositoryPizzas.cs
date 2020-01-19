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
    public class RepositoryPizzas
    {

        private enum size {Small=1, Medium, Large, Extra_Large};
        private enum crust {Original=1, Hand_Tossed, Thin, Stuffed};

        PizzaDBContext db;
        public RepositoryPizzas(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Pizzas item)
        {
            if (db.Pizzas.Any(e => e.PizzaId == item.PizzaId))
            {
                Console.WriteLine("user already with this email exists");
            }
            else
            {
                db.Pizzas.Add(item);
                Console.WriteLine("Pizza craeted successfully");
            }
            db.SaveChanges();
        }

        public IEnumerable<Pizzas> GetItems()
        {
            var query = from e in db.Pizzas
                        select e;

            return query;
        }

      
        
    }
}
