using System;
using System.Collections.Generic;
using System.Linq;

namespace SEHometaskTwo
{
    public class Facade
    {
        public Facade()
        {
            RentPark = new Park();
        }

        public Park RentPark { get; }
        public Car[] GetFreeCars(DateTimeOffset[] desiredRentalPeriod)
        { 
            return RentPark.GetFreeCars(GetDate(desiredRentalPeriod));
        }
        public bool RentCar(int userId, int carId , DateTimeOffset[] desiredRentalPeriod)
        {
            var user = RentPark
                       .Repository
                       .FindUserById(userId) ?? throw new ArgumentException(nameof(userId));
            var car = RentPark
                      .Repository
                      .FindCarById(carId) ?? throw new ArgumentException(nameof(carId));
            return RentPark.RentCar(user, car, GetDate(desiredRentalPeriod));

        }
        public List<Rent> GetUserRentHistory(int userId)
        {
            var user = RentPark
                       .Repository
                       .FindUserById(userId) ?? throw new ArgumentException(nameof(userId));
            return user.Repository.GetRentalHistory();
        }
        public void AddCar(string name)
        {
            var car = new Car(name);
            RentPark.Repository.AddCar(car);
        }
        public void AddUser(string name)
        {
            var user = new User(name);
            RentPark.Repository.AddUser(user);

        }
        private DateTimeOffset[] GetDate(DateTimeOffset[] DateWithTime)
        {
            return DateWithTime.Select(currentDate =>
                   (DateTimeOffset)currentDate.Date).ToArray();
        }
    }
}
