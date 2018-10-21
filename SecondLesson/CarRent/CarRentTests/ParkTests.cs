using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent;

namespace CarRentTests
{
    /// <summary>
    /// Сводное описание для UnitTest1
    /// </summary>
    [TestClass]
    public class ParkTests
    {

        [TestMethod]
        public void CallAddCar_AddCarInPark()
        {
            Park park = new Park(new List<Car>(new Car[] {
                    new Car("toyota supra", 923, 2),
                    new Car("hundai solaris", 223, 9)
                                                              })
              , new List<Inspection>());
            Park expAnsw = new Park(new List<Car>(new Car[] {
                    new Car("toyota supra", 923, 2),
                    new Car("hundai solaris", 223, 9),
                    new Car("bmw x5", 666, 0)
                                                              })
          , new List<Inspection>());


            park.AddCar(new Car("bmw x5", 666, 0));

            for (int i = 0; i < park.Cars.Count; i++)
                Assert.IsTrue(park.Cars[i].Equals(expAnsw.Cars[i]));

        }

        [TestMethod]
        public void CallGoOnInspection_AddCarOnInpection()
        {
            Park park = new Park(new List<Car>(new Car[] {
                    new Car("toyota supra", 923, 2)
                                                              })
              , new List<Inspection>());
            Park expAnsw = new Park(new List<Car>(new Car[] {
                    new Car("toyota supra", 923, 2)
                                                              })
              , new List<Inspection>(new Inspection[] {
                  new Inspection(
                      new DateTimeOffset(1,1,1,1,1,1,1,new TimeSpan()),                     
                      new Car("toyota supra", 923, 2)
                      ) }));

            park.GoOnInspection(park.Cars[0], new DateTimeOffset(1, 1, 1, 1, 1, 1, 1, new TimeSpan()));

            Assert.IsTrue(expAnsw.Insp[0].car.Equals( park.Insp[0].car));
            Assert.IsTrue(expAnsw.Insp[0].Start == park.Insp[0].Start);
            Assert.IsTrue(expAnsw.Insp[0].End == park.Insp[0].End);
        }

        [TestMethod]
        public void CallSeeCarsOnInspection_CarsOnIsp()
        {
            Park park = new Park(new List<Car>(new Car[] {
                    new Car("toyota supra", 923, 2)
                                                              })
              , new List<Inspection>(new Inspection[] {
                  new Inspection(
                      new DateTimeOffset(1,1,1,1,1,1,1,new TimeSpan()),
                      new Car("toyota supra", 923, 2)
                      ) }));
            Car expAnsw = new Car("toyota supra", 923, 2);

            foreach (Car car in park.Cars)
                Assert.IsTrue(car.Equals(expAnsw));
        }
    }
}
