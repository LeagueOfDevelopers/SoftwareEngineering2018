using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEHometaskTwo;

namespace UnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CanRentCarTest_GiveDifferentRentTimes_GetTrueIfRentIsPossible()
        {
            Car lada = new Car("Lada");
            var firstRentPeriodStart = DateTimeOffset.Now.AddYears(1);
            var firstRentPeriodFinish = firstRentPeriodStart.AddMonths(1);
            var firstRentDate = new DateTimeOffset[] { firstRentPeriodStart, firstRentPeriodFinish };
            var possibleRentPeriod = new DateTimeOffset[] {firstRentPeriodFinish.AddMonths(1),
                                     firstRentPeriodFinish.AddMonths(2)};
            var impossibleRentPeriod = new DateTimeOffset[] {firstRentPeriodStart.AddDays(2),
                                       firstRentPeriodFinish};
            var firstRent = new Rent(lada, firstRentDate);
            var user = new User("David");
            user.Repository.AddRent(firstRent);

            var possibleRentPeriodResult = user.CanRentCar(possibleRentPeriod);
            var impossibleRentPeriodResult = user.CanRentCar(impossibleRentPeriod);

            Assert.IsTrue(possibleRentPeriodResult);
            Assert.IsFalse(impossibleRentPeriodResult);
        }
    }
}
