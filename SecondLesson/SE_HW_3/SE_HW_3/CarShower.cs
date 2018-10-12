using System;
using System.Collections.Generic;

namespace SE_HW_3
{
    public class CarShower
    {
        public static List<Car> GetAvailableCars(List<Car> cars)
        {
            List<Car> AvailableCars = new List<Car>();

            foreach (Car car in cars)
            {
                if (car.Status == CarStatus.Free)
                    AvailableCars.Add(car);
            }
            return AvailableCars;
        }
    }
}
