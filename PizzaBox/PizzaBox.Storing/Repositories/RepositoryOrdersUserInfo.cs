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

    public class RepositoryOrdersUserInfo : IRepoNoUpdate<OrdersUserInfo>
    {
        PizzaDBContext db;
        public RepositoryOrdersUserInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add(OrdersUserInfo item)
        {
            //we need to see if user exists
            if (db.Users.Any(e => e.Email == item.Email))
            {
                db.OrdersUserInfo.Add(item);
                Console.WriteLine("Order Created successfully");
            }
            else
            {
                Console.WriteLine("User not found");
            }
            db.SaveChanges();
        }

        public IEnumerable<OrdersUserInfo> GetItems()
        {
            var query = from e in db.OrdersUserInfo
                        select e;

            return query;
        }


        public void Remove(string id)
        {
            
        }
        public IEnumerable<OrdersUserInfo> GetUserPurchases(string email)
        {
            var query = from e in db.OrdersUserInfo
                        where e.Email == email
                        select e;

            return query;
        }
        public OrdersUserInfo GetStoreOrderDetails(int id)
        {
            foreach(OrdersUserInfo o in db.OrdersUserInfo)
            {
                if (o.OrderId == id)
                {
                    return o;
                }
            }
            return null;
        }
    }
}
