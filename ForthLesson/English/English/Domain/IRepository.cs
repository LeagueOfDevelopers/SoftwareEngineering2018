using System;
using System.Collections.Generic;

namespace English.Domain
{
    public interface IRepository<T>
    {
        IEnumerable<T> Items { get; }

        void Save(T item);

        T Load(Guid itemId);
    }
}
