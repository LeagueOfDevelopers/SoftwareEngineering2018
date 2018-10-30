using System;
using LoDSprint.Application;
using LoDSprint.Exceptions;
using LoDSprint.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoDSprintTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(PermissionDeniedException))]
        public void GetUserLearnedWordsTest_FakeUserTryToGetUserWords_ThrowException()
        {
            var userServise = new UserService(
                new InFileUsersRepository("path")
                );
            var getterId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            userServise.GetUserLearnedWords(getterId, userId);
        }

        [TestMethod]
        [ExpectedException(typeof(PermissionDeniedException))]
        public void GetUserStudiedWordsTest_FakeUserTryToGetUserWords_ThrowException()
        {
            var userServise = new UserService(
                new InFileUsersRepository("path")
                );
            var getterId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            userServise.GetUserStudiedWords(getterId, userId);
        }
    }
}
