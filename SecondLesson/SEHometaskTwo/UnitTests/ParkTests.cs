using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEHometaskTwo;

namespace UnitTests
{
    [TestClass]
    public class ParkTests
    {
        [TestMethod]
        public void GetFreeCarsTest_GiveDifferentRentPeriods_GetAllFreeCars()
        {
            var bmw = new Car("Bmw");
            var firstRentPeriodStart = DateTimeOffset.Now.AddYears(1);
            var firstRentPeriodFinish = firstRentPeriodStart.AddMonths(1);
            var firstRentDate = new DateTimeOffset[] { firstRentPeriodStart, firstRentPeriodFinish };
            var possibleRentPeriod = new DateTimeOffset[] {firstRentPeriodFinish.AddMonths(1),
                                     firstRentPeriodFinish.AddMonths(2)};
            var impossibleRentPeriod = new DateTimeOffset[] {firstRentPeriodStart.AddDays(2),
                                       firstRentPeriodFinish};
            bmw.Repository.AddRent(firstRentDate);
            var park = new Park();
            park.Repository.AddCar(bmw);

            var possibleRentPeriodResult = park.GetFreeCars(possibleRentPeriod);
            var impossibleRentPeriodResult = park.GetFreeCars(impossibleRentPeriod);

            Assert.AreEqual(1, possibleRentPeriodResult.Length);
            Assert.AreEqual(0, impossibleRentPeriodResult.Length);
        }
        [TestMethod]
        public void RentCarTest_GiveDifferentRentPeriods_GetTrueIfRentIsSuccefull()
        {
            var bmw = new Car("Bmw");
            var firstRentPeriodStart = DateTimeOffset.Now.AddYears(1);
            var firstRentPeriodFinish = firstRentPeriodStart.AddMonths(1);
            var firstRentDate = new DateTimeOffset[] { firstRentPeriodStart, firstRentPeriodFinish };
            var possibleRentPeriod = new DateTimeOffset[] {firstRentPeriodFinish.AddMonths(1),
                                     firstRentPeriodFinish.AddMonths(2)};
            var impossibleRentPeriod = new DateTimeOffset[] {firstRentPeriodStart.AddDays(2),
                                       firstRentPeriodFinish};
            bmw.Repository.AddRent(firstRentDate);
            var rent = new Rent(bmw, firstRentDate);
            var user = new User("David");
            user.Repository.AddRent(rent);
            bmw.Repository.AddRent(firstRentDate);
            var park = new Park();
            park.Repository.AddCar(bmw);

            var possibleRentResult = park.RentCar(user, bmw, possibleRentPeriod);
            var impossibleRentResult = park.RentCar(user, bmw, impossibleRentPeriod);

            Assert.IsTrue(possibleRentResult);
            Assert.IsFalse(impossibleRentResult);
        }
    }
}
