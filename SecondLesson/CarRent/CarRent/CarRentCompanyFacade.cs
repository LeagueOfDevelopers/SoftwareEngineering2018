using System;
using System.Collections.Generic;

namespace CarRent
{
    public class CarRentCompanyFacade
    {
        public CarRentCompany CarRentCompany { get; }

        public CarRentCompanyFacade()
        {
            CarRentCompany = new CarRentCompany();
        }

        public CarRentCompanyFacade(CarRentCompany carRentOffice)
        {
            CarRentCompany = carRentOffice;
        }

        public List<Car> GetCarsAt(RentTime time)
        {
            return CarRentCompany.GetCarsAt(time);
        }

        public void AddCar(CarFacade carFacade)
        {
            CarRentCompany.AddCar(carFacade);
        }

        public void RemoveCar(CarFacade carFacade)
        {
            CarRentCompany.RemoveCar(carFacade);
        }

        public void SendCarOnRent(CarFacade carFacade, Client client, RentTime time)
        {
            CarRentCompany.SendCarOnRent(carFacade, client, time);
        }

        public void SendCarOnService(CarFacade carFacade, DateTimeOffset startTime)
        {
            CarRentCompany.SendCarOnService(carFacade, startTime);
        }
    }
}
