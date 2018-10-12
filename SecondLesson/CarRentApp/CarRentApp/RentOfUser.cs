using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentApp
{
   public class RentOfUser
    {
        public RentOfUser(string nameOfCar, DateTimeOffset[] userPeriod)
        {
            NameOfCar = nameOfCar ?? throw new ArgumentNullException(nameof(nameOfCar));
            UserPeriod = userPeriod ?? throw new ArgumentNullException(nameof(userPeriod));
        }
        string NameOfCar { get; set; }
        DateTimeOffset[] UserPeriod { get; set; }
    }
}
