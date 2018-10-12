using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRentApp;
using System.Collections.Generic;

namespace CarRentAppTest
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckRepair_ReturnCheck()
        {
            int setRepair = 10;
            DateTimeOffset[] time = { DateTimeOffset.UtcNow, DateTimeOffset.Now };
            List<DateTimeOffset[]> list = new List<DateTimeOffset[]>();
            Car car = new Car("Mazda", list, setRepair);

            bool check = car.CheckRepair(time, setRepair);

            Assert.IsFalse(check);
        }
        [TestMethod]
        public void CheckPeriodOfCar_ReturnCheckTrue()
        {
            List<DateTimeOffset[]> ListOfCar = new List<DateTimeOffset[]>();
            DateTimeOffset date1 = new DateTimeOffset(2019, 1, 2, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset date2 = new DateTimeOffset(2019, 1, 4, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] arrayDate = { date1, date2 };
            ListOfCar.Add(arrayDate);
            DateTimeOffset needDate1 = new DateTimeOffset(2019, 1, 5, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset needDate2 = new DateTimeOffset(2019, 1, 10, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] NeedPeriod = { needDate1, needDate2 };
            Car car = new Car("Honda", ListOfCar);

            bool check = car.CheckPeriod(NeedPeriod);

            Assert.IsTrue(check);
        }

        [TestMethod]
        public void CheckRepairStatus_ReturnTrueIfRepairEqualZero()
        {
            List<DateTimeOffset[]> ListOfCar = new List<DateTimeOffset[]>();
            DateTimeOffset[] arrayDate = { DateTimeOffset.UtcNow, DateTimeOffset.Now };
            Car car = new Car("Subaru", ListOfCar,10);
            int currentcarRepair = car.Repair;
            int expectedRepair = 0;

            car.CheckRepair(arrayDate, currentcarRepair);
            currentcarRepair = car.Repair;

            Assert.AreEqual(expectedRepair, currentcarRepair);
        }
        [TestMethod]
        public void CompareTwoIdOfCars_RetrunTrueIfIsNotEquals()
        {
            List<DateTimeOffset[]> list = new List<DateTimeOffset[]>();
            Car Subaru = new Car("Subaru", list);
            Car Volvo = new Car("Volvo", list);

            int firstId = Subaru.Id;
            int secondId = Volvo.Id;

            Assert.AreNotEqual(firstId, secondId);
        }
    }
}
