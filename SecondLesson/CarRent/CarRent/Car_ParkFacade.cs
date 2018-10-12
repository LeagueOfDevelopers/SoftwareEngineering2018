using System;
using System.Collections.Generic;

namespace CarRent
{
    class Car_ParkFacade
    {
        public Cars_park _cars_park;

        public Car_ParkFacade(Cars_park cars_park)
        {
            _cars_park = cars_park;
        }
        public void AddNewCarInCarPark(Car adding_car)
        {
            _cars_park.AddNewCarInCarPark(adding_car);
        }
        public void RentCar(User tenant, Guid id, DateTimeOffset date_of_begining, DateTimeOffset date_of_ending)
        {
            _cars_park.RentCar(tenant, _cars_park.FindCarByID(id), date_of_begining, date_of_ending);
        }

        public List<Car> ViewListFreeCars(DateTimeOffset date_of_begining, DateTimeOffset date_of_ending)
        {
            return _cars_park.ViewListFreeCars(date_of_begining, date_of_ending);
        }
        public List<Rent> ViewUserRentalHistory(User tenant)
        {
            return _cars_park.ViewUserRentalHistory(tenant);
        }
    }
}
