using System;
using System.Collections.Generic;
using System.Net;

namespace CarsRentSystem
{
   public class Park
   {
      public int       Id       { get; }
      public string    ParkName { get; }
      public List<Car> Cars     { get; } = new List<Car>();

      public Park(int id, string name)
      {
         Id       = id;
         ParkName = name;
      }

      public void AddCar(string model)
      {
         Cars.Add(new Car(Cars.Count, model));
      }

      public List<Car> GetAvailableCars(DateTimeOffset rentStart, DateTimeOffset rentEnd)
      {
         return Cars.FindAll(car => CarAvailable(rentStart, rentEnd, car.Id));
      }

      public bool CarAvailable(DateTimeOffset rentStart, DateTimeOffset rentEnd, int id)
      {
         return Cars.Find(car => car.Id == id && car.CorrectDate(rentStart, rentEnd)) != null;
      }
   }
}