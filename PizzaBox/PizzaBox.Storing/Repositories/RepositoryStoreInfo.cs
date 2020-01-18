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
    public class RepositoryStoreInfo : IRepository<StoreInfo>
    {
        PizzaDBContext db;

        public RepositoryStoreInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(StoreInfo item)
        {
            if (db.StoreInfo.Any(e => e.StoreId == item.StoreId))
            {
                Console.WriteLine("Store with this store id exists");
            }
            else
            {
                db.StoreInfo.Add(item);
                Console.WriteLine("Store craeted successfully");
            }
            db.SaveChanges();
        }

        public IEnumerable<StoreInfo> GetItems()
        {
            var query = from e in db.StoreInfo
                        select e;

            return query;
        }

        public void Modify(StoreInfo item)
        {
            if (db.StoreInfo.Any(e => e.StoreId == item.StoreId))
            {
                StoreInfo updateStore = db.StoreInfo.FirstOrDefault(e => e.StoreId == item.StoreId);
                
            }
            else
            {
                Console.WriteLine("Could not update user because it does not exists");
            }
            db.SaveChanges();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
