using System;
using System.Collections.Generic;

namespace Messenger
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Items { get; }

        T GetItem(Guid itemId);

        void AddItem(T item);

        void SaveItem(T item);

        void UpdateItem(T item);

        void DeleteItem(T item);

        void DeleteItemById(Guid id);
    }
}
