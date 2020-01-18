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
    public class RepositoryPizzas : IRepository<Pizzas>
    {

        PizzaDBContext db;
        public RepositoryPizzas(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Pizzas employee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pizzas> GetItems()
        {
            throw new NotImplementedException();
        }

        public void Modify(Pizzas employee)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
