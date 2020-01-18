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
    public class RepositoryOrdersPizzaInfo : IRepository<OrdersPizzaInfo>
    {
        PizzaDBContext db;
        public RepositoryOrdersPizzaInfo(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add(OrdersPizzaInfo item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdersPizzaInfo> GetItems()
        {
            throw new NotImplementedException();
        }

        public void Modify(OrdersPizzaInfo item)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
