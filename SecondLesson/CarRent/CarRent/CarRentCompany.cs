using System;
using System.Linq;
using System.Collections.Generic;

namespace CarRent
{
    public class CarRentCompany
    {
        public CarRepository CarRepository { get; }
        public ClientRepository ClientRepository { get; }

        public CarRentCompany()
        {
            CarRepository = new CarRepository();
            ClientRepository = new ClientRepository();
        }

        public CarRentCompany(CarRepository carRepository, ClientRepository clientRepository)
        {
            CarRepository = carRepository;
            ClientRepository = clientRepository;
        }

        public List<Car> GetCarsAt(RentTime time)
        {
            var cars = new List<Car>();

            foreach(var car in CarRepository.GetList())
            {
                if (car.IsFreeAt(time))
                {
                    cars.Add(car);
                }
            }

            return cars;
        }

        public void AddCar(CarFacade carFacade)
        {
            CarRepository.Create(carFacade.Car);
        }

        public void RemoveCar(CarFacade carFacade)
        {
            CarRepository.Delete(carFacade.Car);
        }

        public void SendCarOnRent(CarFacade carFacade, Client client, RentTime time)
        {
            CheckCarCanGoOnRent(carFacade, time);

            AddRentHistoryToClient(client, carFacade, time);
            AddRentHistoryToCar(carFacade, time);
        }

        public void SendCarOnService(CarFacade carFacade, DateTimeOffset startTime)
        {
            var endTime = startTime.AddDays(7);
            var time = new RentTime(startTime, endTime);

            if (carFacade.IsFreeAt(time))
            {
                AddServiceHistoryToCar(carFacade, startTime);
            }
            else
            {
                throw new Exception("Машина " + carFacade.Car.Name + " не может быть отправлена на тех обслуживание в " 
                    + startTime + ", так как занята");
            }
        }

        private void AddRentHistoryToClient(Client client, CarFacade carFacade, RentTime time)
        {
            client.RentCar(carFacade, time);
            ClientRepository.Update(client);
        }

        private void AddRentHistoryToCar(CarFacade carFacade, RentTime time)
        {
            carFacade.Car.IncreaseCountToService();
            carFacade.Car.Rents.Add(time);
            CarRepository.Update(carFacade.Car);
        }

        private void AddServiceHistoryToCar(CarFacade carFacade, DateTimeOffset startTime)
        {
            var endTimeOfService = startTime.AddDays(7);
            var serviceTime = new RentTime(startTime, endTimeOfService);
            carFacade.Car.Services.Add(serviceTime);
            carFacade.Car.SetCountToServiceToZero();
            CarRepository.Update(carFacade.Car);
        }

        private void CheckCarCanGoOnRent(CarFacade carFacade, RentTime time)
        {
            if (carFacade.Car.CountToService >= 10)
            {
                throw new Exception("Машину " + carFacade.Car.Name + " нужно отправить на тех осмотр");
            }

            if (carFacade.Car.Rents.Count != 0 &&
                carFacade.Car.Rents.Last().EndTime > time.StartTime)
            {
                throw new Exception("Нельзя отправлять машину " + carFacade.Car.Name +
                    " в аренду до уже существующей c " + time.StartTime + " до " + time.EndTime);
            }

            if (!carFacade.IsFreeAt(time))
            {
                throw new Exception("Машина " + carFacade.Car.Name + " занята в промежутке от "
                    + time.StartTime + " до " + time.EndTime);
            }
        }
    }
}
