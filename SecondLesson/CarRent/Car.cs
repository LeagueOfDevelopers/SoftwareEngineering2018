using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Car
    {
        public Guid Id { get; }
        public string Model { get; }
        public int RentCount { get; private set; }


        public bool WantToRest()
        {
            if (RentCount >= 10)
            {
                RentCount = 0;
                return true;
            }
            else return false;
        }

        public void TakeRent()
        {
            RentCount++;
        }

        public Car(Guid id, string model)
        {
            Id = id;
            Model = model;
            RentCount = 0;
        }

        public Car(Guid id, string model, int rentCount)
        {
            Id = id;
            Model = model;
            RentCount = rentCount;
        }


    }
}
