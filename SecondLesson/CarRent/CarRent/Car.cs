using System;
using System.Collections.Generic;

namespace CarRent
{
    public class Car
    {
        public Guid Id { get; }
        public string Name { get; }
        public List<RentTime> Rents { get; private set; }
        public List<RentTime> Services { get; private set; }
        
        private int _countToService;

        public int CountToService
        {
            get
            {
                return _countToService;
            }
            private set
            {
                if (_countToService > 10)
                {
                    throw new Exception("Машина " + Name + " должна быть отправлена на тех осмотр");
                }
                _countToService = value;
            }
        }

        public Car(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Rents = new List<RentTime>();
            Services = new List<RentTime>();
            CountToService = 0;
        }

        public Car(Guid id, string name, List<RentTime> rentsHistory,
            List<RentTime> servicesHistory, int countToService)
        {
            Id = id;
            Name = name;
            Rents = rentsHistory;
            Services = servicesHistory;
            CountToService = countToService;
        }

        public void IncreaseCountToService()
        {
            CountToService++;
        }

        public void SetCountToServiceToZero()
        {
            CountToService = 0;
        }

        public bool IsAtServiceAt(RentTime time)
        {
            bool isAtService = false;

            foreach(var service in Services)
            {
                if (time.IsCrossedWith(service))
                {
                    isAtService = true;
                }
            }

            return isAtService;
        }

        public bool IsAtRentAt(RentTime time)
        {
            bool isAtRent = false;

            foreach (var rent in Rents)
            {
                if (time.IsCrossedWith(rent))
                {
                    isAtRent = true;
                }
            }

            return isAtRent;
        }

        public bool IsFreeAt(RentTime time)
        {
            return !(IsAtRentAt(time) || IsAtServiceAt(time));
        }
    }
}
