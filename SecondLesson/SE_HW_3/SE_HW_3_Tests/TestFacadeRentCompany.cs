using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE_HW_3;

namespace SE_HW_3_Tests
{
    [TestClass]
    public class TestFacadeRentCompany
    {
        [TestMethod]
        public void TestAddCarToAllCars_CarAddedToList()
        {

            Car testCar = new Car ("Kalina");

            RentCompany rentCompany = new RentCompany(new List<Car>());
            FacadeRentCompany facadeRentCompany = new FacadeRentCompany(rentCompany);

            facadeRentCompany.AddCar(testCar);

            List<Car> testList = new List<Car>{testCar};

            Assert.AreEqual(facadeRentCompany.RentCompany.AllCars[0], testList[0]);
        }


        [TestMethod]
        public void TestCarToService_SendToService()
        {
            User testUser = new User("Someuser", true);
            FacadeUser facadeUser = new FacadeUser(testUser);

            DateTimeOffset dateOfRent = new DateTimeOffset();

            TimeSpan RentDuration = new TimeSpan(7);
            Car TestCar = new Car("Kalina", new Guid(), CarStatus.Rented, 10, 0, dateOfRent, RentDuration);

            facadeUser.EndRent(testUser, TestCar);

            Assert.AreEqual(TestCar.Status, CarStatus.OnService);
        }
    }
}
