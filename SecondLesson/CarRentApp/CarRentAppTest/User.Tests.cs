using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CarRentApp;

namespace CarRentAppTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void CheckPeriodOfUser_ReturnCheckFalse()
        {
            List<DateTimeOffset[]> ListOfUser = new List<DateTimeOffset[]>();
            DateTimeOffset date1 = new DateTimeOffset(2019, 1, 2, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset date2 = new DateTimeOffset(2019, 1, 4, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] arrayDate = { date1, date2 };
            ListOfUser.Add(arrayDate);
            DateTimeOffset needDate1 = new DateTimeOffset(2019, 1, 3, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset needDate2 = new DateTimeOffset(2019, 1, 5, 1, 1, 1, TimeSpan.Zero).Date;
            DateTimeOffset[] NeedPeriod = { needDate1, needDate2 };
            User user = new User("Marshall", ListOfUser);

            bool check = user.CheckPeriod(NeedPeriod);

            Assert.IsFalse(check);
        }
    }
}
