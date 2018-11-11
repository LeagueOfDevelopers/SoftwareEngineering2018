using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public interface IRepository<T>
    {
        void Save(T element);
        T Load(Guid elementID);
        IEnumerable<T> GetAll();
    }
}
