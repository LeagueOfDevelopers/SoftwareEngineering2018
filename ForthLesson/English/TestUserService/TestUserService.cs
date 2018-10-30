using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using English.Application;
using English.Domain.Exception;
using static MethodsForTests.MethodsForTests;

namespace TestUserService
{
    [TestClass]
    public class TestUserService
    {
        [TestMethod]
        public void RegisterNewUser_NewUser()
        {
            var wordId = Guid.NewGuid();
            var user = CreateUserWithWord(wordId, 1);
            var userService = new UserService();

            var newName = "Bob";
            var userId = userService.RegisterNewUser(newName);

            Assert.AreEqual(userService.LoadUser(user.Id).Id, user.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(NameOfUserIsEmptyException))]
        public void RegisterUserWithEmptyName_Exception()
        {
            var wordId = Guid.NewGuid();
            var user = CreateUserWithWord(wordId, 1);
            var userService = new UserService();

            var newName = "";
            userService.RegisterNewUser(newName);
        }
    }
}
