using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE_HW_3;

namespace SE_HW_3_Tests
{
    [TestClass]
    public class TestFacadeUser
    {
        [TestMethod]
        public void TestGetAvailableCars_GetListOfFreeCars()
        {
            User testUser = new User("Someguy", false);
            FacadeUser facadeUser = new FacadeUser(testUser);

            RentCompany rentCompany = new RentCompany(new List<Car>());
            FacadeRentCompany facadeRentCompany = new FacadeRentCompany(rentCompany);
            var car1 = new Car("Solaris");
            var car2 = new Car("Benz");
            var car3 = new Car("Civic");
            var car4 = new Car("Kalina");

            rentCompany.AllCars.Add(car1);
            rentCompany.AllCars.Add(car2); 
            rentCompany.AllCars.Add(car3);
            rentCompany.AllCars.Add(car4);

            List<Car> testListOfCars = new List<Car>{
                car1, car2, car3, car4
            };

            var returnedListOfCars = facadeUser.GetAvailableCars(rentCompany);
            CollectionAssert.AreEqual(testListOfCars, returnedListOfCars);
        }


        [TestMethod]
        public void TestRentCar_ChangeCarStatusOnRented()
        {
            User testUser = new User("Someguy", false);
            FacadeUser facadeUser = new FacadeUser(testUser);

            DateTimeOffset dateOfRent = DateTimeOffset.Now;
            TimeSpan rentDuration = new TimeSpan(7);
            Car testCar = new Car("Kalina", new Guid(), CarStatus.Free, 10, 0, dateOfRent, new TimeSpan(0));

            facadeUser.RentCar(facadeUser.User, testCar, dateOfRent, rentDuration);

            Assert.AreEqual(testCar.Status, CarStatus.Rented);
        }


        [TestMethod]
        public void TestEndRent_SendToService()
        {
            User testUser = new User("Someguy", true);
            FacadeUser facadeUser = new FacadeUser(testUser);

            DateTimeOffset dateOfRent = new DateTimeOffset();
            TimeSpan RentDuration = new TimeSpan(7);
            Car testCar = new Car("Kalina", new Guid(), CarStatus.Rented, 10, 0, dateOfRent, RentDuration);

            facadeUser.EndRent(testUser, testCar);

            Assert.AreEqual(testCar.Status, CarStatus.OnService);
        }

    }
}
