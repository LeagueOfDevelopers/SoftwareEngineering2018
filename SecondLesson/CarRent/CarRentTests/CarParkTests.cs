using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using CarRent;

namespace CarRentTests
{
    [TestClass]
    class AddCarTests
    {
        //add car
        [TestMethod]
        public void AddCar_Car_CarsAppended()
        {
            CarPark carPark = new CarPark("TestPark");
            Car car = new Car("TestCar");

            carPark.AddCar(car);
            Car returnedCar = carPark.Cars[0];

            Assert.AreEqual(car, returnedCar);
        }

        //add car list
        [TestMethod]
        public void AddCar_CarList_CarsAppended()
        {
            CarPark carPark = new CarPark("TestPark");
            List<Car> cars = new List<Car> {
                    new Car("TestCar1"),
                    new Car("TestCar2")
                };

            carPark.AddCar(cars);
            List<Car> returnedCars = carPark.Cars;

            CollectionAssert.AreEquivalent(cars, returnedCars);
        }
    }

    [TestClass]
    class GetUserRecordsTests
    {
        //get user records
        [TestMethod]
        public void GetUserRecords_NoRecs_EmptyReturned()
        {
            CarPark carPark = new CarPark("TestPark");
            User user = new User("TestUser");
            string[] expectedStrRecs = { };

            string[] StrRecs = carPark.GetUserStringRecords(user);

            CollectionAssert.AreEquivalent(StrRecs, expectedStrRecs);
        }

        [TestMethod]
        public void GetUserRecords_OneRec_StrArrReturned()
        {
            CarPark carPark = new CarPark("TestPark");
            User user = new User("TestUser");
            Car car = new Car("TestCar");
            DateTimeOffset startDate = DateTimeOffset.Now.AddDays(1);
            DateTimeOffset endDate = startDate.AddDays(10);

            carPark.AddCar(car);
            carPark.RentCar(car, startDate, endDate, user);
            /*
               "Car name: " + Car.Name
                                         + ". Car ID: " + Car.Id.ToString()
                                         + ". Start date: " + Period.StartDate.ToString("d")
                                         + ". End date: " + Period.EndDate.ToString("d")
                                         + ". Record ID: " + Id.ToString()
                                         + ". Reason: " + (User != null ? "user" : "repair"
            */

            string[] StrRecs = carPark.GetUserStringRecords(user);
            string rec = StrRecs[0];

            StringAssert.Contains(rec, car.Id.ToString());
            StringAssert.Contains(rec, startDate.ToString("d"));
            StringAssert.Contains(rec, endDate.ToString("d"));
            StringAssert.Contains(rec, user.Id.ToString());
            StringAssert.Contains(rec, "user");

        }
    }

    [TestClass]
    class RentCarTests
    {
        //rent car
        [TestMethod]
        public void RentCar_CarWithNormalPeriod_TrueReturned()
        {
            CarPark carPark = new CarPark("TestPark");
            User user = new User("TestUser");
            Car car = new Car("TestCar");
            DateTimeOffset startDate = DateTimeOffset.Now.AddDays(1);
            DateTimeOffset endDate = startDate.AddDays(10);

            carPark.AddCar(car);
            bool succes = carPark.RentCar(car, startDate, endDate, user);

            Assert.IsTrue(succes);
        }

        [TestMethod]
        public void RentCar_CarWithInappropriatePeriod_FalseReturned()
        {
            CarPark carPark = new CarPark("TestPark");
            User user = new User("TestUser");
            Car car = new Car("TestCar");
            DateTimeOffset startDate = DateTimeOffset.Now.AddDays(-1);
            DateTimeOffset endDate = DateTimeOffset.Now.AddDays(1);

            carPark.AddCar(car);
            bool succes = carPark.RentCar(car, startDate, endDate, user);

            Assert.IsFalse(succes);
        }
        //check repair
        [TestMethod]
        public void RentCar_TenthPeriod_FalseReturned()
        {
            CarPark carPark = new CarPark("TestPark");
            User user = new User("TestUser");
            Car car = new Car("TestCar");
            DateTimeOffset startDate = DateTimeOffset.Now.AddDays(1);
            DateTimeOffset endDate = startDate.AddDays(1);

            carPark.AddCar(car);

            for (int i = 0; i < 10; i++, startDate = startDate.AddDays(2), endDate = endDate.AddDays(2))
            {
                bool yeldedRes = carPark.RentCar(car, startDate, endDate, user);
                Assert.IsTrue(yeldedRes);
            }

            string[] UserRecs = carPark.GetUserStringRecords(user);
            Assert.AreEqual(10, UserRecs.Length);

            bool succes = carPark.RentCar(car, endDate.AddDays(5), endDate.AddDays(6), user);

            Assert.IsFalse(succes);
        }
    }

    [TestClass]
    class GetAvailableCarListTests
    {
        //get available cars list period
        [TestMethod]
        public void GetAvailableCarsList_OneCar_CarListReturned()
        {
            CarPark carPark = new CarPark("TestPark");
            User user = new User("TestUser");
            Car car = new Car("TestCar");
            List<Car> expectedList = new List<Car> { car };
            DateTimeOffset startDate = DateTimeOffset.Now.AddDays(1);
            DateTimeOffset endDate = startDate.AddDays(1);

            carPark.AddCar(car);

            List<Car> availableCars = carPark.GetAvailableCarsList(startDate, endDate);

            CollectionAssert.AreEquivalent(expectedList, availableCars);
        }
    }
}
