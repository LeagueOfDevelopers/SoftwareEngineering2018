using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEHometaskTwo;

namespace UnitTests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void CanBeRentedTest_GiveDifferentRentedPeriod_GetTrueIfCarCanBeRented()
        {
            var lada = new Car("Lada");
            var firstRentPeriodStart = DateTimeOffset.Now.AddYears(1);
            var firstRentPeriodFinish = firstRentPeriodStart.AddMonths(1);
            var firstRentDate = new DateTimeOffset[] { firstRentPeriodStart, firstRentPeriodFinish };
            var possibleRentPeriod = new DateTimeOffset[] {firstRentPeriodFinish.AddMonths(1),
                                     firstRentPeriodFinish.AddMonths(2)};
            var impossibleRentPeriod = new DateTimeOffset[] {firstRentPeriodStart.AddDays(2),
                                       firstRentPeriodFinish};
            lada.Repository.AddRent(firstRentDate);

            var possibleRentResult = lada.CanBeRented(possibleRentPeriod);
            var impossibleRentResult = lada.CanBeRented(impossibleRentPeriod);

            Assert.IsTrue(possibleRentResult);
            Assert.IsFalse(impossibleRentResult);
        }
        [TestMethod]
        public void SendForRepairTest_GiveCarForRepair_CheckInaccessibleForBeforePeriod()
        {
            var needRepairCar = new Car("UAZ");
            var beforeRentPeriodStart = DateTimeOffset.Now.AddYears(1);
            var beforeRentPeriodFinish = beforeRentPeriodStart.AddMonths(1);
            var beforeRentDate = new DateTimeOffset[] { beforeRentPeriodStart, beforeRentPeriodFinish };
            var firstRentDate = new DateTimeOffset[] { beforeRentPeriodFinish.AddMonths(1), beforeRentPeriodFinish.AddMonths(2) };
            needRepairCar.Repository.AddRent(firstRentDate);

            needRepairCar.SendForRepair();
            var canBeRanted = needRepairCar.CanBeRented(beforeRentDate);

            Assert.IsFalse(canBeRanted);
        }
    }
}
