using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent;

namespace CarStorageTest
{
    [TestClass]
    public class CarStorageTest
    {
        [TestMethod]
        public void RentCar10Times_CarAtService()
        {
            var carRentCompanyFacade = new CarRentCompanyFacade();
            var carFacade = new CarFacade("10");
            var clientFacade = new ClientFacade("Bob");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 10, 07));
            var date3 = new DateTimeOffset(new DateTime(2018, 10, 08));
            var date4 = new DateTimeOffset(new DateTime(2018, 10, 09));
            var date5 = new DateTimeOffset(new DateTime(2018, 10, 10));
            var date6 = new DateTimeOffset(new DateTime(2018, 10, 11));
            var date7 = new DateTimeOffset(new DateTime(2018, 10, 12));
            var date8 = new DateTimeOffset(new DateTime(2018, 10, 13));
            var date9 = new DateTimeOffset(new DateTime(2018, 10, 14));
            var date10 = new DateTimeOffset(new DateTime(2018, 10, 15));
            var date11 = new DateTimeOffset(new DateTime(2018, 10, 16));
            var date12 = new DateTimeOffset(new DateTime(2018, 10, 17));
            var date13 = new DateTimeOffset(new DateTime(2018, 10, 18));
            var date14 = new DateTimeOffset(new DateTime(2018, 10, 19));
            var date15 = new DateTimeOffset(new DateTime(2018, 10, 20));
            var date16 = new DateTimeOffset(new DateTime(2018, 10, 21));
            var date17 = new DateTimeOffset(new DateTime(2018, 10, 22));
            var date18 = new DateTimeOffset(new DateTime(2018, 10, 23));
            var date19 = new DateTimeOffset(new DateTime(2018, 10, 24));
            var date20 = new DateTimeOffset(new DateTime(2018, 10, 25));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date3, date4);
            var time3 = new RentTime(date5, date6);
            var time4 = new RentTime(date7, date8);
            var time5 = new RentTime(date9, date10);
            var time6 = new RentTime(date11, date12);
            var time7 = new RentTime(date13, date14);
            var time8 = new RentTime(date15, date16);
            var time9 = new RentTime(date17, date18);
            var time10 = new RentTime(date19, date20);

            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time1);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time2);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time3);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time4);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time5);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time6);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time7);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time8);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time9);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time10);
            carRentCompanyFacade.SendCarOnService(carFacade, time10.EndTime.AddDays(1));

            Assert.AreEqual(carFacade.Car.Services.Count, 1);
            Assert.AreEqual(carFacade.Car.CountToService, 0);
        }

        [TestMethod]
        public void SendCarOnRent11Times_Exception()
        {
            var carRentCompanyFacade = new CarRentCompanyFacade();
            var carFacade = new CarFacade("10");
            var clientFacade = new ClientFacade("Bob");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 10, 07));
            var date3 = new DateTimeOffset(new DateTime(2018, 10, 08));
            var date4 = new DateTimeOffset(new DateTime(2018, 10, 09));
            var date5 = new DateTimeOffset(new DateTime(2018, 10, 10));
            var date6 = new DateTimeOffset(new DateTime(2018, 10, 11));
            var date7 = new DateTimeOffset(new DateTime(2018, 10, 12));
            var date8 = new DateTimeOffset(new DateTime(2018, 10, 13));
            var date9 = new DateTimeOffset(new DateTime(2018, 10, 14));
            var date10 = new DateTimeOffset(new DateTime(2018, 10, 15));
            var date11 = new DateTimeOffset(new DateTime(2018, 10, 16));
            var date12 = new DateTimeOffset(new DateTime(2018, 10, 17));
            var date13 = new DateTimeOffset(new DateTime(2018, 10, 18));
            var date14 = new DateTimeOffset(new DateTime(2018, 10, 19));
            var date15 = new DateTimeOffset(new DateTime(2018, 10, 20));
            var date16 = new DateTimeOffset(new DateTime(2018, 10, 21));
            var date17 = new DateTimeOffset(new DateTime(2018, 10, 22));
            var date18 = new DateTimeOffset(new DateTime(2018, 10, 23));
            var date19 = new DateTimeOffset(new DateTime(2018, 10, 24));
            var date20 = new DateTimeOffset(new DateTime(2018, 10, 25));
            var date21 = new DateTimeOffset(new DateTime(2018, 10, 26));
            var date22 = new DateTimeOffset(new DateTime(2018, 10, 27));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date3, date4);
            var time3 = new RentTime(date5, date6);
            var time4 = new RentTime(date7, date8);
            var time5 = new RentTime(date9, date10);
            var time6 = new RentTime(date11, date12);
            var time7 = new RentTime(date13, date14);
            var time8 = new RentTime(date15, date16);
            var time9 = new RentTime(date17, date18);
            var time10 = new RentTime(date19, date20);
            var time11 = new RentTime(date21, date22);

            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time1);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time2);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time3);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time4);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time5);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time6);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time7);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time8);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time9);
            carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time10);

            bool hasException = false;
            try
            {
                carRentCompanyFacade.SendCarOnRent(carFacade, clientFacade.Client, time11);
            }
            catch(Exception)
            {
                hasException = true;
            }

            Assert.IsTrue(hasException);
        }

        [TestMethod]
        public void GetCarsAt_Cars()
        {
            var carRentFacade = new CarRentCompanyFacade();
            var carFacade1 = new CarFacade("10");
            var carFacade2 = new CarFacade("Lava Kavina");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 07));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 07));
            var time = new RentTime(date1, date2);

            carRentFacade.CarRentCompany.CarRepository.Create(carFacade1.Car);
            carRentFacade.CarRentCompany.CarRepository.Create(carFacade2.Car);
            
            var cars = carRentFacade.GetCarsAt(time);

            Assert.AreEqual(cars.ToArray()[0].Name, carFacade1.Car.Name);
            Assert.AreEqual(cars.ToArray()[1].Name, carFacade2.Car.Name);
        }

        [TestMethod]
        public void AddCar_NewCarInList()
        {
            var carRentFacade = new CarRentCompanyFacade();
            var carFacade = new CarFacade("Lava Kavina");

            carRentFacade.AddCar(carFacade);

            Assert.AreEqual(carRentFacade.CarRentCompany.CarRepository.GetList().First().Name,
                carFacade.Car.Name);
        }

        [TestMethod]
        public void RemoveCar_LessCarInList()
        {
            var carRentFacade = new CarRentCompanyFacade();
            var carFacade = new CarFacade("Lava Kavina");
            carRentFacade.CarRentCompany.CarRepository.Create(carFacade.Car);

            carRentFacade.RemoveCar(carFacade);

            Assert.AreEqual(carRentFacade.CarRentCompany.CarRepository.GetSize(), 0);
        }

        [TestMethod]
        public void SendCarOnRent_CarOnRent()
        {
            var carRentFacade = new CarRentCompanyFacade();
            var clientFacade = new ClientFacade("Bob");
            var carFacade = new CarFacade("Lava Kavina");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 07));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 07));
            var time = new RentTime(date1, date2);

            carRentFacade.CarRentCompany.CarRepository.Create(carFacade.Car);

            carRentFacade.SendCarOnRent(carFacade, clientFacade.Client, time);

            Assert.AreEqual(carRentFacade.CarRentCompany.CarRepository.GetItem(0).IsAtRentAt(time), true);
        }

        [TestMethod]
        public void SendCarOnService_CarOnService()
        {
            var carRentFacade = new CarRentCompanyFacade();
            var clientFacade = new ClientFacade("Bob");
            var carFacade = new CarFacade("Lava Kavina");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 07));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 07));
            var time = new RentTime(date1, date2);

            carRentFacade.CarRentCompany.CarRepository.Create(carFacade.Car);

            carRentFacade.SendCarOnService(carFacade, time.StartTime);
            
            Assert.AreEqual(carRentFacade.CarRentCompany.CarRepository.GetItem(0).IsAtServiceAt(time), true);
        }
    }
}
