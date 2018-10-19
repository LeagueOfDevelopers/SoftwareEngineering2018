using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent;

namespace ClientTest
{
    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        public void RentCar_CarInRentHistory()
        {
            var clientFacade = new ClientFacade("Bob");
            var carFacade = new CarFacade("Audi");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            clientFacade.RentCar(carFacade, time);
            
            Assert.AreEqual(carFacade, clientFacade.Client.RentHistory.First().Value);
        }

        [TestMethod]
        public void RentCarAtUsedTime_Exception()
        {
            var clientFacade = new ClientFacade("Bob");
            var carFacade1 = new CarFacade("Audi");
            var carFacade2 = new CarFacade("BMW");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var time = new RentTime(date1, date2);

            clientFacade.RentCar(carFacade1, time);

            bool hasException = false;
            try
            {
                clientFacade.RentCar(carFacade2, time);
            }
            catch (Exception)
            {
                hasException = true;
            }

            Assert.IsTrue(hasException);
        }

        [TestMethod]
        public void RentCarAtCrossedUsedTime_Exception()
        {
            var clientFacade = new ClientFacade("Bob");
            var carFacade1 = new CarFacade("Audi");
            var carFacade2 = new CarFacade("BMW");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var date3 = new DateTimeOffset(new DateTime(2018, 10, 19));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date1, date3);

            clientFacade.RentCar(carFacade1, time1);
            bool hasException = false;
            try
            {
                clientFacade.RentCar(carFacade2, time2);
            }
            catch (Exception)
            {
                hasException = true;
            }

            Assert.IsTrue(hasException);
        }

        [TestMethod]
        public void RentTwoCarsByOnClient_TwoCarsInRentHistory()
        {
            var clientFacade = new ClientFacade("Bob");
            var carFacade1 = new CarFacade("Audi");
            var carFacade2 = new CarFacade("BMW");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var date3 = new DateTimeOffset(new DateTime(2018, 11, 10));
            var date4 = new DateTimeOffset(new DateTime(2018, 11, 19));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date3, date4);

            clientFacade.RentCar(carFacade1, time1);
            clientFacade.RentCar(carFacade2, time2);

            var listOfRent = clientFacade.Client.RentHistory.ToList();

            Assert.AreEqual(listOfRent[0].Value, carFacade1);
            Assert.AreEqual(listOfRent[0].Key.StartTime, time1.StartTime);
            Assert.AreEqual(listOfRent[0].Key.EndTime, time1.EndTime);

            Assert.AreEqual(listOfRent[1].Value, carFacade2);
            Assert.AreEqual(listOfRent[1].Key.StartTime, time2.StartTime);
            Assert.AreEqual(listOfRent[1].Key.EndTime, time2.EndTime);
        }

        [TestMethod]
        public void HasCarAt_True()
        {
            var answer = true;
            var clientFacade = new ClientFacade("Bob");
            var carFacade = new CarFacade("Audi");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var date3 = new DateTimeOffset(new DateTime(2018, 10, 10));
            var date4 = new DateTimeOffset(new DateTime(2018, 11, 16));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date3, date4);

            clientFacade.RentCar(carFacade, time1);
            var hasCar = clientFacade.HasCarAt(time2);

            Assert.AreEqual(answer, hasCar);
        }

        [TestMethod]
        public void HasCarAt_False()
        {
            var answer = false;
            var clientFacade = new ClientFacade("Bob");
            var carFacade = new CarFacade("Audi");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 11, 06));
            var date3 = new DateTimeOffset(new DateTime(2018, 11, 10));
            var date4 = new DateTimeOffset(new DateTime(2022, 01, 13));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date3, date4);

            clientFacade.RentCar(carFacade, time1);
            var hasCar = clientFacade.HasCarAt(time2);

            Assert.AreEqual(answer, hasCar);
        }

        [TestMethod]
        public void RentLotsOfCars_ClientsWithRentHistory()
        {
            var clientFacade1 = new ClientFacade("Bob");
            var carFacade1 = new CarFacade("1");
            var carFacade2 = new CarFacade("2");
            var date1 = new DateTimeOffset(new DateTime(2018, 10, 06));
            var date2 = new DateTimeOffset(new DateTime(2018, 10, 10));
            var date3 = new DateTimeOffset(new DateTime(2018, 10, 30));
            var date4 = new DateTimeOffset(new DateTime(2018, 11, 02));
            var date5 = new DateTimeOffset(new DateTime(2018, 12, 01));
            var date6 = new DateTimeOffset(new DateTime(2018, 12, 31));
            var time1 = new RentTime(date1, date2);
            var time2 = new RentTime(date3, date4);
            var time3 = new RentTime(date5, date6);

            var clientFacade2 = new ClientFacade("Alice");
            var carFacade3 = new CarFacade("3");
            var carFacade4 = new CarFacade("4");
            var carFacade5 = new CarFacade("5");
            var carFacade6 = new CarFacade("6");
            var date7 = new DateTimeOffset(new DateTime(2018, 10, 15));
            var date8 = new DateTimeOffset(new DateTime(2018, 11, 30));
            var date9 = new DateTimeOffset(new DateTime(2018, 12, 06));
            var date10 = new DateTimeOffset(new DateTime(2018, 12, 27));
            var date11 = new DateTimeOffset(new DateTime(2019, 03, 01));
            var date12 = new DateTimeOffset(new DateTime(2019, 04, 14));
            var date13 = new DateTimeOffset(new DateTime(2019, 11, 10));
            var date14 = new DateTimeOffset(new DateTime(2020, 11, 19));
            var time4 = new RentTime(date7, date8);
            var time5 = new RentTime(date9, date10);
            var time6 = new RentTime(date11, date12);
            var time7 = new RentTime(date13, date14);

            clientFacade1.RentCar(carFacade1, time1);
            clientFacade1.RentCar(carFacade2, time2);
            clientFacade1.RentCar(carFacade1, time3);

            clientFacade2.RentCar(carFacade3, time4);
            clientFacade2.RentCar(carFacade4, time5);
            clientFacade2.RentCar(carFacade5, time6);
            clientFacade2.RentCar(carFacade6, time7);

            Assert.AreEqual(clientFacade1.GetHistory().Count, 3);
            Assert.AreEqual(clientFacade2.GetHistory().Count, 4);
        }
    }
}
