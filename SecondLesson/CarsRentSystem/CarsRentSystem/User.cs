using System;
using System.Collections.Generic;

namespace CarsRentSystem
{
   public class User
   {
      public int    Id   { get; }
      public string Name { get; }

      private List<Check> History = new List<Check>();

      public User(int id, string name)
      {
         Id   = id;
         Name = name;
      }

      public void RentCar(Park park, int id, DateTimeOffset rentStart, DateTimeOffset rentEnd)
      {
         if (CorrectDate(rentStart, rentEnd) && park.CarAvailable(rentStart, rentEnd, id))
         {
            park.Cars.Find(car => car.Id == id).Rent(rentStart, rentEnd);
            History.Add(new Check(park.ParkName, id, rentStart, rentEnd)); 
         }
      }

      public List<Check> GetHistory()
      {
         return History;
      }

      private bool CorrectDate(DateTimeOffset rentStart, DateTimeOffset rentEnd)
      {
         return rentStart < rentEnd && !DatesIntersect(rentStart, rentEnd);
      }
      
      private bool DatesIntersect(DateTimeOffset rentStart, DateTimeOffset rentEnd)
      {
         return History.FindAll(check => !(rentStart > check.RentEnd || rentEnd < check.RentStart)).Count > 0;
      }
   }
}