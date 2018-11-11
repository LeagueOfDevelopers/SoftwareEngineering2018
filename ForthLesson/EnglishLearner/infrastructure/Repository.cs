using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public class Repository<T> : IRepository<T> where T : IIdentificator
    {
        private Dictionary<Guid, T> elements;

        public Repository(IEnumerable<T> elements)
        {
            if(elements == null) throw new ArgumentNullException(nameof(elements));
            foreach(T element in elements)
            {
                this.elements[element.Id] = element;
            }
        }
      
        public IEnumerable<T> GetAll()
        {
            return elements.Values;
        }

        public T Load(Guid elementID)
        {
            if (!elements.Keys.Contains(elementID)) throw new InvalidOperationException("Element not found");
            return elements[elementID];
        }

        public void Save(T element)
        {
            elements[element.Id] = element; 
        }

    }
}
