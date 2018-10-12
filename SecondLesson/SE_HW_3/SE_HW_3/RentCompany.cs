using System;
using System.Collections.Generic;

namespace SE_HW_3
{
    public class RentCompany
    {
        public List<Car> AllCars { get; private set; }

        public RentCompany(List<Car> allCars)
        {
            AllCars = allCars;
        }


        public static void SendToService(Car car)
        {
            car.ChangeStatus(car, CarStatus.OnService);
            car.ServiceCount++;
        }


        public void AddCar(string model)
        {
            AllCars.Add(new Car(model));
        }
    }
}
