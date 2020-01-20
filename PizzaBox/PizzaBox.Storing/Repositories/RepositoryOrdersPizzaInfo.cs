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
    public class RepositoryOrdersPizzaInfo : IRepoNoUpdate<OrdersPizzaInfo>
    {
        PizzaDBContext db;
        public RepositoryOrdersPizzaInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add(OrdersPizzaInfo item)
        {
            if (db.Pizzas.Any(e => e.PizzaId == item.PizzaId) && db.OrdersUserInfo.Any(e => e.OrderId == item.OrderId))
            {
                db.OrdersPizzaInfo.Add(item);
            }
            else
            {
                Console.WriteLine("OrderID  or PizzaID not found");
            }
            db.SaveChanges();
        }

        public IEnumerable<OrdersPizzaInfo> GetItems()
        {
            var query = from e in db.OrdersPizzaInfo
                        select e;

            return query;
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdersPizzaInfo> GetOrdersPizzas(int id)
        {
            var query = from e in db.OrdersPizzaInfo
                        where e.OrderId == id
                        select e;

            return query;
        }
    }
}
