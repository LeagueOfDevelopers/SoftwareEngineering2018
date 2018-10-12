using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRent
{
    [TestClass]
    public class RentTest
    {
        [TestMethod]
        public void RentConstructTest_DateInStr()
        {
            DateTimeOffset StartExp = new DateTimeOffset(2000, 3, 31, 0, 0, 0, new TimeSpan(0));
            DateTimeOffset FinishExp = new DateTimeOffset(2000, 4, 20, 0, 0, 0, new TimeSpan(0));
            string StartRes = "31.03.2000";
            string FinishRes = "20.04.2000";

            User user = new User(new Guid(), "test");
            Car car = new Car(new Guid(), "test");

            Rent Expected = new Rent(StartExp, FinishExp, user, car);
            Rent Result = new Rent(StartRes, FinishRes, user, car);

            bool flag = (((Expected.Start == Result.Start)) && ((Expected.Finish == Result.Finish)));

            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void RentConstructTest_DateSwitch()
        {
            DateTimeOffset StartExp = new DateTimeOffset(2000, 3, 31, 0, 0, 0, new TimeSpan(0));
            DateTimeOffset FinishExp = new DateTimeOffset(2000, 4, 20, 0, 0, 0, new TimeSpan(0));
            
            User user = new User(new Guid(), "test");
            Car car = new Car(new Guid(), "test");

            Rent Expected = new Rent(StartExp, FinishExp, user, car);
            Rent Result = new Rent(FinishExp, StartExp, user, car);

            bool flag = (((Expected.Start == Result.Start)) && ((Expected.Finish == Result.Finish)));

            Assert.IsTrue(flag);
        }
    }
}
