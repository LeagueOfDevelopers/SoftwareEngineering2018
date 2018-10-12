using System;
using System.Collections.Generic;

namespace SE_HW_3
{
    public class User
    {
        public string Name { get; }
        public bool DoesHaveRent { get; private set; }

        public List<Car> UserHistory { get; private set; }

        public User(string name, bool doesHaveRent)
        {
            Name = name;
            DoesHaveRent = doesHaveRent;
            UserHistory = new List <Car>();
        }


        public void RentCar(User user, Car car, DateTimeOffset startRent, TimeSpan durationRent)
        {
            if (car.Status != CarStatus.Free || user.DoesHaveRent )
                return;

            car.ChangeStatus(car, CarStatus.Rented);
            car.RentDuration = durationRent;
            car.RentStart = startRent;

            user.UserHistory.Add(car);
            user.DoesHaveRent = true;
        }


        public void EndRent(User user, Car car)
        {
            if (car.Status == CarStatus.Free || !user.DoesHaveRent)
                return;

            car.RentCount++;
            car.ChangeStatus(car, CarStatus.Free);
            user.DoesHaveRent = false;

            if (car.RentCount > 10)
            {
                car.ChangeStatus(car, CarStatus.OnService);
                car.ServiceCount = 0;
            }
        }
    }
}
