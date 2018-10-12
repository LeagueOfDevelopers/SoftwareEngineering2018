using System;
using System.Collections.Generic;

namespace CarsRentSystem
{
   public class Car
   {
      public int    Id    { get; }
      public string Model { get; }
      
      public DateTimeOffset BeginRentDate { get; private set; } = DateTimeOffset.Now;
      public int Quality { get; private set; } = 10;

      public List<UnavailableDate> RentDates   { get; } = new List<UnavailableDate>();
      public List<UnavailableDate> RepairDates { get; } = new List<UnavailableDate>();

      public Car(int id, string model)
      {
         Id    = id;
         Model = model;
      }

      public void Rent(DateTimeOffset rentStart, DateTimeOffset rentEnd)
      {
         DecrementQuality();  
         if (!CheckQuality())
            GoRepairAfterRent(rentEnd);
         
         RentDates.Add(new UnavailableDate(rentStart, rentEnd));
      }

      public bool CorrectDate(DateTimeOffset rentStart, DateTimeOffset rentEnd)
      {
         return rentStart < rentEnd && !DatesIntersect(rentStart, rentEnd) && rentStart > BeginRentDate;
      }
      
      private bool DatesIntersect(DateTimeOffset rentStart, DateTimeOffset rentEnd)
      {
         return RentDates.FindAll(date => !(rentStart > date.End || rentEnd < date.Start)).Count > 0 ||
                RepairDates.FindAll(date => !(rentStart > date.End || rentEnd < date.Start)).Count > 0;
      }
      
      private void GoRepairAfterRent(DateTimeOffset rentEnd)
      {
         RepairDates.Add(new UnavailableDate(rentEnd, rentEnd.AddDays(7)));
         BeginRentDate = rentEnd.AddDays(7);
         ResetQuality();
      }
      
      private bool CheckQuality()
      {
         return Quality > 0;
      }
      
      private void DecrementQuality()
      {
         Quality--;
      }
      
      private void ResetQuality()
      {
         Quality = 10;
      }
   }
}