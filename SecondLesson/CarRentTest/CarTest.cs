using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRent
{
    [TestClass]
    public class CarTest
    {
        [TestMethod]
        public void WantToRestTest_10true()
        {
            Car TestCar = new Car(new Guid(), "Toyota");
            bool Expected = true;

            for (int i = 0; i < 10; i++)
            {
                TestCar.TakeRent();
            }
            bool Result = TestCar.WantToRest();

            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void WantToRestTest_9false()
        {
            Car TestCar = new Car(new Guid(), "Toyota");
            bool Expected = false;

            for (int i = 0; i < 9; i++)
            {
                TestCar.TakeRent();
            }
            bool Result = TestCar.WantToRest();

            Assert.AreEqual(Expected, Result);
        }

        [TestMethod]
        public void WantToRestTest_zeroing()
        {
            Car TestCar = new Car(new Guid(), "Toyota");
            int Expected = 0;

            for (int i = 0; i < 10; i++)
            {
                TestCar.TakeRent();
            }
            TestCar.WantToRest();
            int Result = TestCar.RentCount;

            Assert.AreEqual(Expected, Result);
        }
    }
}
