using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CarRentApp;

namespace CarRentAppTest
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void AddNewDatesToCar_ReturnTrueIfContainsNewDates()
        {
            List<DateTimeOffset[]> ListOfUser = new List<DateTimeOffset[]>();
            DateTimeOffset userDate1 = new DateTimeOffset(2019, 1, 2, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset userDate2 = new DateTimeOffset(2019, 1, 4, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] arrayDateWithUser = { userDate1, userDate2 };
            ListOfUser.Add(arrayDateWithUser);
            List<DateTimeOffset[]> ListOfCar = new List<DateTimeOffset[]>();
            DateTimeOffset carDate1 = new DateTimeOffset(2019, 1, 5, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset carDate2 = new DateTimeOffset(2019, 1, 10, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] arrayDateWithCar = { carDate1, carDate2 };
            ListOfCar.Add(arrayDateWithCar);
            DateTimeOffset promisingDate1 = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset promisingDate2 = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] promisingDate = { promisingDate1, promisingDate2 };
            List<Car> Cars = new List<Car>();
            User user = new User("John", ListOfUser);
            Car car = new Car("Toyota", ListOfCar);
            Park park = new Park(Cars);
            Cars.Add(car);

            park.RentCar(user, car, promisingDate);
            List<DateTimeOffset[]> expectedListOfCar = car.ListOfCar;
            List<DateTimeOffset[]> expectedListOfUser = user.ListOfUser;

            CollectionAssert.Contains(expectedListOfCar, promisingDate);
        }
        [TestMethod]
        public void AddNewDatesToUser_ReturnTrueIfContainsNewDates()
        {
            List<DateTimeOffset[]> ListOfUser = new List<DateTimeOffset[]>();
            DateTimeOffset userDate1 = new DateTimeOffset(2019, 1, 2, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset userDate2 = new DateTimeOffset(2019, 1, 4, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] arrayDateWithUser = { userDate1, userDate2 };
            ListOfUser.Add(arrayDateWithUser);
            List<DateTimeOffset[]> ListOfCar = new List<DateTimeOffset[]>();
            DateTimeOffset carDate1 = new DateTimeOffset(2019, 1, 5, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset carDate2 = new DateTimeOffset(2019, 1, 10, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] arrayDateWithCar = { carDate1, carDate2 };
            ListOfCar.Add(arrayDateWithCar);
            DateTimeOffset promisingDate1 = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset promisingDate2 = new DateTimeOffset(2020, 1, 1, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] promisingDate = { promisingDate1, promisingDate2 };
            List<Car> Cars = new List<Car>();
            User user = new User("John", ListOfUser);
            Car car = new Car("Toyota", ListOfCar);
            Park park = new Park(Cars);
            Cars.Add(car);

            park.RentCar(user, car, promisingDate);
            List<DateTimeOffset[]> expectedListOfCar = car.ListOfCar;
            List<DateTimeOffset[]> expectedListOfUser = user.ListOfUser;

            CollectionAssert.Contains(expectedListOfUser, promisingDate);
        }
        [TestMethod]
        public void CheckFreeCars_GetCountOfFreeCars()
        {
            List<Car> Cars = new List<Car>();
            List<DateTimeOffset[]> ListOfCar = new List<DateTimeOffset[]>();
            DateTimeOffset carDate1 = new DateTimeOffset(2019, 1, 5, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset carDate2 = new DateTimeOffset(2019, 1, 10, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] datesForFirstCar = { carDate1, carDate2 };
            ListOfCar.Add(datesForFirstCar);
            Car car = new Car("BMW", ListOfCar);
            List<DateTimeOffset[]> ListOfCar2 = new List<DateTimeOffset[]>();
            DateTimeOffset carDate3 = new DateTimeOffset(2019, 2, 5, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset carDate4 = new DateTimeOffset
                (2019, 2, 9, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] datesForSecondCar = { carDate3, carDate4};
            ListOfCar2.Add(datesForFirstCar);
            ListOfCar2.Add(datesForSecondCar);
            Car car1 = new Car("Volvo", ListOfCar2);
            DateTimeOffset wishfulRentDate1 = new DateTimeOffset(2019, 2, 1, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset wishfulRentDate2 = new DateTimeOffset(2019, 2, 10, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] wishfulDates = { wishfulRentDate1, wishfulRentDate2 };
            Cars.Add(car);
            Cars.Add(car1);
            Park park = new Park(Cars);

            List<Car> FreeCars =  park.GetFreeCars(wishfulDates);

            CollectionAssert.Contains(FreeCars, car);
        }
    }
}
