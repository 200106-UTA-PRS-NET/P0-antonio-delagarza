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
    public class RepositoryStoreOrdersInfo : IRepoNoUpdate<StoreOrdersInfo>
    {

        PizzaDBContext db;
        public RepositoryStoreOrdersInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(StoreOrdersInfo item)
        {

            //We need to see if the store id and order id exist
            if (db.StoreInfo.Any(e => e.StoreId == item.StoreId) && db.OrdersUserInfo.Any(e => e.OrderId == item.OrderId))
            {
                db.StoreOrdersInfo.Add(item);
            }
            else
            {
                Console.WriteLine("OrderID  or StoreID not found");
            }
            db.SaveChanges();
        }

        public IEnumerable<StoreOrdersInfo> GetItems()
        {
            var query = from e in db.StoreOrdersInfo
                        select e;

            return query;
        }


        public void Remove(string id)
        {
            
        }
    }
}
