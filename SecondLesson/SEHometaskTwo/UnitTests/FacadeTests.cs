using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEHometaskTwo;
namespace UnitTests
{
    [TestClass]
    public class FacadeTests
    {
        [TestMethod]
        public void RentCarTest_GiveNonexSictentIds_GetArgumentException()
        {
            var facade = new Facade();
            var rentPeriodStart = DateTimeOffset.Now;
            var rentPeriodFinish = rentPeriodStart.AddMonths(1);
            var rentPeriod = new DateTimeOffset[] { rentPeriodStart, rentPeriodFinish };
            var nonexistentId = 999999;

            Assert.ThrowsException<ArgumentException>(() => facade.RentCar(nonexistentId, nonexistentId, rentPeriod));
        }
        [TestMethod]
        public void GetUserRentHistoryTest_GiveNonexistentId_GetArgumentException()
        {
            var facade = new Facade();
            var nonexistentId = 9999999;

            Assert.ThrowsException<ArgumentException>(() => facade.GetUserRentHistory(nonexistentId));
        }
        [TestMethod]
        public void AddUserTest_AddNewUsersAndCheckIds_GetCorrectIds()
        {
            var facade = new Facade();

            facade.AddUser("David");
            facade.AddUser("Anton");
            var firstUser = facade.RentPark
                            .Repository.Users
                            .Find(user => user.Name == "David");
            var secondUser = facade.RentPark
                            .Repository.Users
                            .Find(user => user.Name == "Anton");

            Assert.AreEqual(firstUser.Id + 1, secondUser.Id);
        }
        [TestMethod]
        public void AddCarTest_AddNewCarsAndCheckIds_GetCorrectIds()
        {
            var facade = new Facade();

            facade.AddCar("Vaz-2107");
            facade.AddCar("Vaz-2106");
            var firstCar = facade.RentPark
                           .Repository.AllCars
                           .Find(car => car.Name == "Vaz-2107");
            var secondCar = facade.RentPark
                           .Repository.AllCars
                           .Find(car => car.Name == "Vaz-2106");
            Assert.AreEqual(firstCar.Id + 1, secondCar.Id);

        }

    }
}
