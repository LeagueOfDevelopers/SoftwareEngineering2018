using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CarRent;

namespace CarRentTests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void CallAddCountUsed_AddCountUsed()
        {
            Car car = new Car("toyota", 923, 2);
            int expAnsw = 3;

            car.AddCountUsed();

            Assert.IsTrue(expAnsw == car.CountUsed);

        }

    }
   
}
