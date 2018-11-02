using System;
using System.Collections.Generic;
using System.Linq;
using English.Domain;
using English.Domain.Exception;

namespace English.Infrastructure
{
    public class ItemRepository<T> : IRepository<T> where T : IStoredItem
    {
        private readonly List<T> _items;

        public ItemRepository(List<T> items)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public IEnumerable<T> Items => _items;

        public T Load(Guid itemId)
        {
            var searchedItem = _items.FirstOrDefault(item => item.Id == itemId);

            if (searchedItem == null)
            {
                throw new ItemNotFoundException(itemId, _items.GetType().FullName);
            }

            return searchedItem;
        }

        public void Save(T item)
        {
            if (!_items.Contains(item))
            {
                _items.Add(item);
            }
        }
    }
}
