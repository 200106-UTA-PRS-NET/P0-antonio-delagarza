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
    public class RepositoryStoreOrdersInfo : IRepository<StoreOrdersInfo>
    {

        PizzaDBContext db;
        public RepositoryStoreOrdersInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(StoreOrdersInfo employee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StoreOrdersInfo> GetItems()
        {
            throw new NotImplementedException();
        }

        public void Modify(StoreOrdersInfo employee)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
