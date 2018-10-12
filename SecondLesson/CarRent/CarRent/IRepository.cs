using System.Collections.Generic;

namespace CarRent
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetList();

        T GetItem(string name);

        T GetItem(int position);

        int GetSize();

        void Create(T item);

        void Update(T item);
        
        void Delete(T item);
    }
}
