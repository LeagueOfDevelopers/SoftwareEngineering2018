using System;

namespace Messenger
{
    interface IRepository<T> where T : class
    {
        void Create(T item);
        T Get(Guid id);
        void Remove(Guid id);
        void Save(T item);
    }
}
