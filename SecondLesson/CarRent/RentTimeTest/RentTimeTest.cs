using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent;

namespace RentTimeTest
{
    [TestClass]
    public class RentTimeTest
    {
        [TestMethod]
        public void CreateRentTime_ObjectRentTime()
        {
            var data1 = new DateTimeOffset(new DateTime(2018, 10, 07));
            var data2 = new DateTimeOffset(new DateTime(2018, 12, 15));

            var rentTime = new RentTime(data1, data2);

            Assert.AreEqual(rentTime.StartTime, data1);
            Assert.AreEqual(rentTime.EndTime, data2);
        }

        [TestMethod]
        public void CreateWrongRentTime_Exception()
        {
            var data1 = new DateTimeOffset(new DateTime(2018, 12, 15));
            var data2 = new DateTimeOffset(new DateTime(2018, 10, 07));

            bool hasException = false;
            try
            {
                var rentTime = new RentTime(data1, data2);
            }
            catch(Exception)
            {
                hasException = true;
            }

            Assert.IsTrue(hasException);
        }
    }
}
