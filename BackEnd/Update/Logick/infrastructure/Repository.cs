using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner.infrastructure
{
    public class Repository<T> : IRepository<T> where T : IIdentificator
    {
        private Dictionary<Guid, T> elements;

        public IEnumerable<T> GetRandomElements(int count)
        {
            Random r = new Random();
            int maxIndex = elements.Values.Count - 1;
            int[] indexes = new int[count];

            indexes.Select(index => {
                int i;
                while (indexes.Contains((i = r.Next(maxIndex))));
                return i;
            });

            return indexes.Select(index => elements.Values.ElementAt(index));   
        }

        public Repository(IEnumerable<T> elements)
        {
            if(elements == null) throw new ArgumentNullException(nameof(elements));
            if (elements.Count() == 0)
            {
                this.elements = new Dictionary<Guid, T>();
            }
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
