using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEHometaskTwo;

namespace UnitTests
{
    [TestClass]
    public class RentTests
    {
        [TestMethod]
        public void IsPossibleTest_GiveDifferentRentPeriods_GetTrueIfRentIsPossible()
        {
            var audi = new Car("Audi");
            var firstRentPeriodStart = DateTimeOffset.Now.AddYears(1);
            var firstRentPeriodFinish = firstRentPeriodStart.AddMonths(1);
            var firstRentDate = new DateTimeOffset[] { firstRentPeriodStart, firstRentPeriodFinish };
            var possibleRentPeriod = new DateTimeOffset[] {firstRentPeriodFinish.AddMonths(1),
                                     firstRentPeriodFinish.AddMonths(2)};
            var impossibleRentPeriod = new DateTimeOffset[] {firstRentPeriodStart.AddDays(2),
                                       firstRentPeriodFinish};
            var firstRent = new Rent(audi, firstRentDate);

            var possibleRentPeriodResult = firstRent.IsPossible(possibleRentPeriod);
            var impossibleRentPeriodResult = firstRent.IsPossible(impossibleRentPeriod);

            Assert.IsTrue(possibleRentPeriodResult);
            Assert.IsFalse(impossibleRentPeriodResult);
        }
    }
}
