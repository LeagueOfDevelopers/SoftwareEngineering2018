using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class Cars_ParkRepository : IRepository<Cars_park>
    {
        private List<Cars_park> _cars_list;
        public Cars_ParkRepository()
        {
            _cars_list = new List<Cars_park>();
        }

        public Cars_ParkRepository(List<Cars_park> cars_list)
        {
            _cars_list = cars_list;
        }

        public void Create(Cars_park park)
        {
            _cars_list.Add(park);
        }

        public void Delete(int id)
        {
            _cars_list.RemoveAt(id);
        }
       

        public IEnumerable<Cars_park> GetAll()
        {
            return _cars_list;
        }
       
    }
}
