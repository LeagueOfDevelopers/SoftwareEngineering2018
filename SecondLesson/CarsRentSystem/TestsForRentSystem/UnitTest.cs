using System;
using Xunit;
using CarsRentSystem;

namespace TestsForRentSystem
{
   public class UnitTest1
   {
      [Fact]
      public void TestRentCarMethod_NewCheck()
      {
         var App = CreateDefaultApp();
         var user = App.Users[0];
         var park = App.Parks[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.True(user.GetHistory().Count == 1);
      }
      
      [Fact]
      public void TestRentCarMethod_CarUnavailable()
      {
         var App = CreateDefaultApp();
         var user = App.Users[0];
         var park = App.Parks[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.False(park.CarAvailable(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), 0));
      }
      
      [Fact]
      public void TestRentCarMethod_CarAvailable()
      {
         var App = CreateDefaultApp();
         var user = App.Users[0];
         var park = App.Parks[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now.AddDays(1), DateTimeOffset.Now);
         
         Assert.True(park.CarAvailable(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), 0));
      }
      
      [Fact]
      public void TestRentCarMethod_EmptyChecks()
      {
         var App = CreateDefaultApp();
         var user = App.Users[0];
         var park = App.Parks[0];
         
         user.RentCar(park, 5, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.True(user.GetHistory().Count == 0);
      }
      
      [Fact]
      public void TestRentCarMethod_OneRented()
      {
         var App = CreateDefaultApp();
         var user = App.Users[0];
         var park = App.Parks[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 2, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 3, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.True(user.GetHistory().Count == 1);
      }

      [Fact]
      public void TestRentCarMethod_AllRented()
      {
         var App = CreateDefaultApp();
         var user = App.Users[0];
         var park = App.Parks[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 1, DateTimeOffset.Now.AddDays(2), DateTimeOffset.Now.AddDays(3));
         user.RentCar(park, 2, DateTimeOffset.Now.AddDays(4), DateTimeOffset.Now.AddDays(5));
         user.RentCar(park, 3, DateTimeOffset.Now.AddDays(6), DateTimeOffset.Now.AddDays(7));
         
         Assert.True(user.GetHistory().Count == 4);
      }
      
      [Fact]
      public void TestGetAvailableCarsMethod_NoAvailable()
      {
         var App = CreateDefaultApp();
         var user = App.Users[0];
         var park = App.Parks[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 1, DateTimeOffset.Now.AddDays(2), DateTimeOffset.Now.AddDays(3));
         user.RentCar(park, 2, DateTimeOffset.Now.AddDays(4), DateTimeOffset.Now.AddDays(5));
         user.RentCar(park, 3, DateTimeOffset.Now.AddDays(6), DateTimeOffset.Now.AddDays(7));
         
         Assert.True(park.GetAvailableCars(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(7)).Count == 0);
      }
      
      [Fact]
      public void TestGetAvailableCarsMethod_AllAvailable()
      {
         var App = CreateDefaultApp();
         var user = App.Users[0];
         var park = App.Parks[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 2, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 3, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.True(park.GetAvailableCars(DateTimeOffset.Now.AddDays(2), DateTimeOffset.Now.AddDays(3)).Count == 4);
      }
      
      [Fact]
      public void TestGetAvailableCarsMethod_NoAvailableDueToDate()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         
         Assert.True(park.GetAvailableCars(DateTimeOffset.Now.AddDays(1), DateTimeOffset.Now).Count == 0);
      }
      
      [Fact]
      public void TestCarAvailableMethod_Available()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.True(park.CarAvailable(DateTimeOffset.Now.AddDays(1), DateTimeOffset.Now.AddDays(2), 0));
      }
      
      [Fact]
      public void TestCarAvailableMethod_NotAvailable()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.False(park.CarAvailable(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), 0));
      }
      
      [Fact]
      public void TestCarAvailableMethod_NotAvailableDueToDates()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.False(park.CarAvailable(DateTimeOffset.Now.AddDays(2), DateTimeOffset.Now.AddDays(1), 0));
      }
      
      [Fact]
      public void TestCarAvailableMethod_NotAvailableDueToRepair()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];
         
         for (var i = 0; i < 10; i++)
         {
            user.RentCar(park, 0, DateTimeOffset.Now.AddDays(i), DateTimeOffset.Now.AddDays(i + 1));
         }
         
         Assert.False(park.CarAvailable(DateTimeOffset.Now.AddDays(12), DateTimeOffset.Now.AddDays(15), 0));
      }
      
      [Fact]
      public void TestCorrectDatesCarMethod_IncorrectDueToDates()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         
         Assert.False(park.Cars.Find(car => car.Id == 0).CorrectDate(DateTimeOffset.Now.AddDays(2), DateTimeOffset.Now.AddDays(1)));
      }
      
      [Fact]
      public void TestCorrectDatesCarMethod_IncorrectDueToRent()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.False(park.Cars.Find(car => car.Id == 0).CorrectDate(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1)));
      }
      
      [Fact]
      public void TestCorrectDatesCarMethod_IncorrectDueToBeginRent()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         
         Assert.False(park.Cars.Find(car => car.Id == 0).CorrectDate(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(2)));
      }
      
      [Fact]
      public void TestCorrectDatesCarMethod_Correct()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         
         Assert.True(park.Cars.Find(car => car.Id == 0).CorrectDate(DateTimeOffset.Now.AddDays(1), DateTimeOffset.Now.AddDays(2)));
      }
      
      [Fact]
      public void TestRentMethod_QualityDecrementsOnTwo()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];
         
         user.RentCar(park, 0, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
         user.RentCar(park, 0, DateTimeOffset.Now.AddDays(1), DateTimeOffset.Now.AddDays(2));
         
         Assert.True(park.Cars.Find(car => car.Id == 0).Quality == 8);
      }
      
      [Fact]
      public void TestRentMethod_NewRepairDate()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];

         for (var i = 0; i < 10; i++)
         {
            user.RentCar(park, 0, DateTimeOffset.Now.AddDays(i), DateTimeOffset.Now.AddDays(i + 1));
         }
         
         Assert.True(park.Cars.Find(car => car.Id == 0).RepairDates.Count == 1);
      }
      
      [Fact]
      public void TestRentMethod_FullQuality()
      {
         var App = CreateDefaultApp();
         var park = App.Parks[0];
         var user = App.Users[0];

         for (var i = 0; i < 10; i++)
         {
            user.RentCar(park, 0, DateTimeOffset.Now.AddDays(i), DateTimeOffset.Now.AddDays(i + 1));
         }
         
         Assert.True(park.Cars.Find(car => car.Id == 0).Quality == 10);
      }
      
      private static Application CreateDefaultApp()
      {
         var App = new Application("Car Rent System");

         var user = App.CreateUser("name");
         var park = App.CreatePark("park");
         
         park.AddCar("lada");
         park.AddCar("renault");
         park.AddCar("porsche");
         park.AddCar("mitsubishi");

         return App;
      }
   }
}