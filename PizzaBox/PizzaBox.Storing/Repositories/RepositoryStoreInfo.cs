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
                Console.WriteLine("Could not update store because it does not exists");
            }
            db.SaveChanges();
        }
        
        public void Remove(string id)
        {
            int idInt = Convert.ToInt32(id);
            StoreInfo s = db.StoreInfo.FirstOrDefault(e => e.StoreId == idInt);
            if (s.StoreId == idInt)
            {
                db.Remove(s);
                db.SaveChanges();
                Console.WriteLine("User Removed Successfully");
            }
            else
            {
                Console.WriteLine("Could not find store with this Id");
                return;
            }
        }

        public int NumStores()
        {
            return db.StoreInfo.Count();
        }

        public void SetStore(int id, ref StoreInfo st)
        {
            foreach (StoreInfo store in db.StoreInfo)
            {
                if (store.StoreId == id)
                {
                    st = new StoreInfo()
                    {
                        StoreId = store.StoreId,
                        StoreName = store.StoreName,
                        Address = store.Address,
                        City = store.City,
                        State = store.State,
                        ZipCode = store.ZipCode,
                        StorePrice = store.StorePrice
                    };
                    return;
                }
            }
            st = null;
        }

        public bool FindStore(int id)
        {
            foreach (StoreInfo store in db.StoreInfo)
            {
                if (store.StoreId == id)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetStoreName(int id)
        {
            foreach (StoreInfo store in db.StoreInfo)
            {
                if (store.StoreId == id)
                {
                    return store.StoreName;
                }
            }
            return "";
        }
    }
}
