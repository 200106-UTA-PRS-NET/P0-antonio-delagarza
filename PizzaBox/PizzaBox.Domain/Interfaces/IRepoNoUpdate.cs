using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Interfaces
{
    public interface IRepoNoUpdate<T>
    {
        IEnumerable<T> GetItems();
        void Add(T item);
        void Remove(string id);
    }
}
