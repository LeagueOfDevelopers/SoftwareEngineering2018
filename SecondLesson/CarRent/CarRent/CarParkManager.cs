using System;
using CarRent;

namespace CarParkManager
{
    //Fasade
    public class CarParkManager
    {
        private CarPark CarPark { get; set; }

        public CarParkManager(string CarParkName)
        {
            CarPark = new CarPark(CarParkName);
        }

        public string RentCar(Car car, DateTimeOffset startDate, DateTimeOffset endDate, User user)
        {
            bool result = CarPark.RentCar(car, startDate.Date, endDate.AddDays(0.99f).Date, user);

            if (result == true)
            {
                return "Car rented";
            }
            else
            {
                //Можно ввести коды ошибок
                return "Inappropriate period or user";
            }
        }

        public string AddCar(Car car)
        {
            CarPark.AddCar(car);
            return "Car Added";
        }

        public string GetUserRecords(User user)
        {
            return String.Join('\n', CarPark.GetUserStringRecords(user));
        }

        public string GetAvailableCars(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            string[] CarsArray = CarPark.GetAvailableCarsList(startDate.Date, endDate.AddDays(0.99f).Date)
                                        .ConvertAll((Car car) => car.ToString()).ToArray();

            string Cars = String.Join('\n', CarsArray);

            return Cars;
        }

    }
}
