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

    public class RepositoryOrdersUserInfo : IRepository<OrdersUserInfo>
    {
        PizzaDBContext db;
        public RepositoryOrdersUserInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add(OrdersUserInfo item)
        {
            
        }

        public IEnumerable<OrdersUserInfo> GetItems()
        {
            throw new NotImplementedException();
        }

        public void Modify(OrdersUserInfo item)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
