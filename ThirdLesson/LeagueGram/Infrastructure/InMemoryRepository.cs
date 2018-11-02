using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueGram.Infrastructure
{
	public class InMemoryRepository<T>
	{
		public IEnumerable<T> GetAll()
		{
			return _elements.Values;
		}

		public T Load(Guid id)
		{
			return _elements[id];
		}

		public void Save(Guid id, T element)
		{
			_elements[id] = element;
		}

		private Dictionary<Guid, T> _elements;
	}
}
