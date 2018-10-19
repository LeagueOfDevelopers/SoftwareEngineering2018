using System;

namespace CarRent
{
    public class CarFacade
    {
        public Car Car { get; }

        public CarFacade(string name)
        {
            Car = new Car(name);
        }

        public CarFacade(Car car)
        {
            Car = car;
        }

        public bool IsAtServiceAt(RentTime time)
        {
            return Car.IsAtServiceAt(time);
        }

        public bool IsAtRentAt(RentTime time)
        {
            return Car.IsAtRentAt(time);
        }

        public bool IsFreeAt(RentTime time)
        {
            return Car.IsFreeAt(time);
        }
    }
}
