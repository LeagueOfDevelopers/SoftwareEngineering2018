using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Program
    {
        static void Main(string[] args)
        {
            Facade facade = new Facade(CreateRepository());
        }
        static public Repository CreateRepository()
        {
            Repository repa = new Repository(GetContracts(), GetUsers(), GetPark());
            return repa;
        }
        static public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            return users;
        }
        static public List<RentContract> GetContracts()
        {
            List<RentContract> contract = new List<RentContract>();
            return contract;
        }
        static public Park GetPark()
        {
            Park park = new Park(new List<Car>(), new List<Inspection>());
            return park;
        }

    }
}
