using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public struct Car
    {
        public Car(string name, int id, int countUsed)
        {
            Name = name ;
            Id = id;
            CountUsed = countUsed;
        }

        public string Name { get; }
        public int Id { get; }
        public int CountUsed { get; private set; }

        public void AddCountUsed()
        {
            CountUsed++;
        }
       
        public bool Equals(Car car)
        {
            return (car.CountUsed == CountUsed) && (car.Id == Id) && (car.Name == Name);
        }
        
    }
}
