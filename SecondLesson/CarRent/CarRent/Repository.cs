using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Repository
    {
        public Repository(List<RentContract> contracts, List<User> users, Park park)
        {
            Contracts = contracts ?? throw new ArgumentNullException(nameof(contracts));
            Users = users ?? throw new ArgumentNullException(nameof(users));
            Park = park ?? throw new ArgumentNullException(nameof(park));
        }

        public List<RentContract> Contracts { get; set; }
        public List<User> Users { get; set; }
        public Park Park { get; set; }
    }
}
