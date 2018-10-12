using System;
using System.Collections.Generic;

namespace CarRent
{
    public class Client
    {
        public string Id { get; }
        public string Name { get; }
        public Dictionary<RentTime, CarFacade> RentHistory { get; private set; }

        public Client(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            RentHistory = new Dictionary<RentTime, CarFacade>();
        }

        public Client(string id, string name, Dictionary<RentTime, CarFacade> ownedCarHistory)
        {
            Id = id;
            Name = name;
            RentHistory = ownedCarHistory;
        }

        public void RentCar(CarFacade carFacade, RentTime time)
        {
            if (HasCarAt(time))
            {
                throw new Exception("У клиента " + Name + " уже есть машина в аренде во это время: " + time);
            }
            else
            {
                RentHistory.Add(time, carFacade);
            }
        }

        public bool HasCarAt(RentTime time)
        {
            bool hasCar = false;

            foreach(var rent in RentHistory)
            {
                if (time.IsCrossedWith(rent.Key))
                {
                    hasCar = true;
                }
            }

            return hasCar;
        }
    }
}
