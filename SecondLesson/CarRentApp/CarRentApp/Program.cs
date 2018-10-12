using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarRentApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<DateTimeOffset[]> time = new List<DateTimeOffset[]>();
            User user = new User("NameOfUser", time);
            List<Car> cars = new List<Car>();
            Park park = new Park(cars);
            Facade facade = new Facade(user, park);
        }
    }
}
