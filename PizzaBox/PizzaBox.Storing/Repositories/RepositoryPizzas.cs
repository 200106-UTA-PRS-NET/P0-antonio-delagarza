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

        public enum SizeAvailable {Small=1, Medium, Large, Extra_Large};
        public enum CrustAvailable { Original=1, Hand_Tossed, Thin, Stuffed};
        public enum CrustFlavorAvailable { No_Flavor=1, Garlic_Buttery_Blend, Toasted_Parmesan };
        public enum SauceAvailable { Marinara=1, Creamy_Garlic_Parmesan, Barbeque, Buffalo };
        public enum AmountsAvailable { Light = 1, Regular, Extra};
        public enum ToppingsAvailable
        { Pepperoni = 1, Italial_Sausage, Meatball, Ham, Bacon, Grilled_Chicken, Beef, Pork,
                                Mushroom, Spinach, Onion, Olives, Green_Bell_Peppers, Banana_Peppers, Pineapple, Jalapenos, Tomatoes};

        PizzaDBContext db;
        public RepositoryPizzas(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Pizzas item)
        {
            if (db.Pizzas.Any(e => e.PizzaId == item.PizzaId))
            {
                Console.WriteLine("Pizza with this id already exists");
            }
            else
            {
                db.Pizzas.Add(item);
                db.SaveChanges();
                
                
            }
            
        }

        public IEnumerable<Pizzas> GetItems()
        {
            var query = from e in db.Pizzas
                        select e;

            return query;
        }

        public Pizzas GetPizzasbyId(int id)
        {
            foreach(Pizzas p in db.Pizzas)
            {
                if (p.PizzaId == id)
                {
                    return p;
                }
            }
            return null;
        }
      
        
    }
}
