using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent;

namespace CarTest
{
    [TestClass]
    public class CarTest
    {
        [TestMethod]
        public void IsAtServiceAtFreeTime_False()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            var isAtService = carFacade.IsAtServiceAt(time);

            Assert.IsFalse(isAtService);
        }

        [TestMethod]
        public void IsAtServiceAtBusyTime_True()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            carFacade.Car.Services.Add(time);
            var isAtService = carFacade.IsAtServiceAt(time);

            Assert.IsTrue(isAtService);
        }

        [TestMethod]
        public void IsAtServiceAtBusyCrossedTime_True()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var date3 = new DateTimeOffset(new DateTime(2018, 10, 31));
            var date4 = new DateTimeOffset(new DateTime(2019, 02, 26));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date3, date4);

            carFacade.Car.Services.Add(time1);
            var isAtService = carFacade.IsAtServiceAt(time2);

            Assert.IsTrue(isAtService);
        }

        [TestMethod]
        public void IsAtRentAtFreeTime_False()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            var isAtRent = carFacade.IsAtRentAt(time);

            Assert.IsFalse(isAtRent);
        }

        [TestMethod]
        public void IsAtRentAtBusyTime_True()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            carFacade.Car.Rents.Add(time);
            var isAtRent = carFacade.IsAtRentAt(time);

            Assert.IsTrue(isAtRent);
        }

        [TestMethod]
        public void IsAtRentAtCrossedTime_True()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var date3 = new DateTimeOffset(new DateTime(2018, 10, 31));
            var date4 = new DateTimeOffset(new DateTime(2019, 02, 26));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date3, date4);

            carFacade.Car.Rents.Add(time1);
            var isAtRent = carFacade.IsAtRentAt(time2);

            Assert.IsTrue(isAtRent);
        }

        [TestMethod]
        public void IsFreeAtFreeTime_True()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            var isFreeAt = carFacade.IsFreeAt(time);

            Assert.IsTrue(isFreeAt);
        }

        [TestMethod]
        public void IsFreeAtServiceTime_False()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            carFacade.Car.Services.Add(time);
            var isFreeAt = carFacade.IsFreeAt(time);

            Assert.IsFalse(isFreeAt);
        }

        [TestMethod]
        public void IsFreeAtRentTime_False()
        {
            var carFacade = new CarFacade("10");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            carFacade.Car.Rents.Add(time);
            var isFreeAt = carFacade.IsFreeAt(time);

            Assert.IsFalse(isFreeAt);
        }
    }
}
