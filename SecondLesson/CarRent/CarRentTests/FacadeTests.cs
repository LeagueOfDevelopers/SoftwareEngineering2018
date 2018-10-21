using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent;
using System.Collections.Generic;

namespace CarRentTests
{
    [TestClass]
    public class FacadeTests
    {
        [TestMethod]
        public void CallSeeFreeCars_GetFreeCars()
        {
            Car[] cars = new Car[]{
                    new Car("toyota supra", 923, 2),
                    new Car("hundai solaris", 223, 9),
                    new Car("bmw x5", 666, 0) };
            Facade facade = new Facade(new Repository(
                new List<RentContract>(new RentContract[] {
                    new RentContract(
                        new DateTimeOffset(1, 1, 1, 1, 1, 1, 1, new TimeSpan()),
                        new DateTimeOffset(1, 1, 3, 1, 1, 1, 1, new TimeSpan()),
                        new User("Koly",1),
                        new Car("bmw x5", 666, 0)
                )}),
                new List<User>(new User[] {
                new User("Koly",1)}),
                new Park(new List<Car>(cars),
                new List<Inspection>(new Inspection[] {
                    new Inspection(new DateTimeOffset(1, 1, 1, 0, 0, 0, 0, new TimeSpan()),new Car("toyota supra", 923, 2))
                })
                )
                ));
            List<Car> car = new List<Car>(new Car[] { cars[1] });


            List<Car> answ = facade.SeeFreeCars(
                new DateTimeOffset(1, 1, 2, 0, 0, 0, 0, new TimeSpan()),
                new DateTimeOffset(1, 1, 3, 0, 0, 0, 0, new TimeSpan())
                );

            for(int i = 0; i < answ.Count; i++)
            {
                Assert.IsTrue(answ[i].Equals(car[i]));
            }

        }

        [TestMethod]
        public void CallRentCar_AddCarInContract()
        {
            Car[] cars = new Car[]{
                    new Car("toyota supra", 923, 2),
                    new Car("hundai solaris", 223, 9),
                    new Car("bmw x5", 666, 0) };
            Facade facade = new Facade(new Repository(
                new List<RentContract>(),
                new List<User>(new User[] {
                new User("Koly",1)}),
                new Park(new List<Car>(cars),
                new List<Inspection>()
                )
                ));
            List<RentContract> expAnsw = new List<RentContract>( new RentContract[] { new RentContract(
                  new DateTimeOffset(1, 1, 1, 0, 0, 0, 0, new TimeSpan()),
                  new DateTimeOffset(1, 1, 2, 0, 0, 0, 0, new TimeSpan()),
                  new User("Koly",1),
                  new Car("toyota supra", 923, 2))}
                );

            facade.RentCar(
                new Car("toyota supra", 923, 2),
                new User("Koly", 1),
                new DateTimeOffset(1, 1, 1, 0, 0, 0, 0, new TimeSpan()),
                new DateTimeOffset(1, 1, 2, 0, 0, 0, 0, new TimeSpan()));

            for (int i = 0; i < facade.repa.Contracts.Count; i++)
                Assert.IsTrue(facade.repa.Contracts[i].car.Equals(expAnsw[i].car));
        }
        [TestMethod]
        public void GetRentHistory_addCar()
        {
            
        }
    }
}
