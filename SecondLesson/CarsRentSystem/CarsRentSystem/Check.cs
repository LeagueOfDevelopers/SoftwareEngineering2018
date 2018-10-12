using System;

namespace CarsRentSystem
{
   public class Check
   {
      public string Park  { get; }
      public int    CarId { get; }
      
      public DateTimeOffset RentStart { get; }
      public DateTimeOffset RentEnd   { get; }

      public Check(string park, int carId, DateTimeOffset rentStart, DateTimeOffset rentEnd)
      {
         Park      = park;
         CarId     = carId;
         RentStart = rentStart;
         RentEnd   = rentEnd;
      }
   }
}