using System;
using System.Linq;


namespace SEHometaskTwo
{
    public class Park
    {
        public Park()
        {
            Repository = new ParkRepository();
        }

        public ParkRepository Repository { get; }
        public Car[] GetFreeCars (DateTimeOffset[] desiredRentalPeriod)
        {
            return Repository.GetAllCars()
                   .Where(car =>
                   car.CanBeRented(desiredRentalPeriod)).ToArray();
        }
        public bool RentCar(User user, Car car, DateTimeOffset[] rentalPeriod)
        {
            if (car.CanBeRented(rentalPeriod) && user.CanRentCar(rentalPeriod))
            {
                car.Repository.AddRent(rentalPeriod);
                car.IncreaseDroveWithoutRepair();
                var newRent = new Rent(car, rentalPeriod);
                user.Repository.AddRent(newRent);
                if (car.NeedRepair())
                {
                    car.SendForRepair();
                }
                return true;

            }
            else
                return false;
        }
    }
}
