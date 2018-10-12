using System;

namespace CarsRentSystem
{
   public class UnavailableDate
   {
      public DateTimeOffset Start { get; }
      public DateTimeOffset End   { get; }

      public UnavailableDate(DateTimeOffset start, DateTimeOffset end)
      {
         Start = start;
         End   = end;
      }
   }
}