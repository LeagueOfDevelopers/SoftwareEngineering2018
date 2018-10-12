using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentApp
{
    public class Facade
    {
        User user;
        Park park;

        public Facade(User user, Park park)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            this.park = park ?? throw new ArgumentNullException(nameof(park));
        }

        public void GetFreeCars(DateTimeOffset[] wishfulDate)
        {
            park.GetFreeCars(wishfulDate);
        }
        public void RentCar(User user, Car car, DateTimeOffset[] promisingDate)
        {
            park.RentCar(user, car, promisingDate);
        }
        public void GetHistoryOfUserRent()
        {
            user.GetHistoryOfUserRent();
        }
        public void AddNewCar(string Name)
        {
            park.AddNewCar(Name);
        }
    }
}
