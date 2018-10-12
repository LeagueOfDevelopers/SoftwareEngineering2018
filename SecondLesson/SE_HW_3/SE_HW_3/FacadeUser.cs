using System;
using System.Collections.Generic;

namespace SE_HW_3
{
    public class FacadeUser
    {

        public User User{ get; }

        public FacadeUser(User user){
            User = user;
        }

        public List<Car> GetAvailableCars(RentCompany rentCompany)
        {
            return CarShower.GetAvailableCars(rentCompany.AllCars);
        }


        public void RentCar(User user, Car car, DateTimeOffset startRent, TimeSpan durationRent)
        {
            User.RentCar(user, car, startRent, durationRent);
        }


        public void EndRent(User user, Car car)
        {
            User.EndRent(user, car);
        }
    }
}
