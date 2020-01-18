using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetItems();
        void Add(T item);
        void Modify(T item);
        void Remove(string id);
    }
}
