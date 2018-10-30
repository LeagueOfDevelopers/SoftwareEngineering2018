using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoDSprint.Application;
using LoDSprint.Repositories;
using LoDSprint;
using System;
using System.Collections.Generic;
using LoDSprint.Exceptions;

namespace LoDSprintTests
{
    [TestClass]
    public class AdministratorServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(PermissionDeniedException))]
        public void AddNewWordTest_NotAdministratorTryToAddNewUser_throwException()
        {
            var administrator = new User(
                    Guid.NewGuid(),
                    "admin",
                    new List<Word>(),
                    new Dictionary<Word, int>()
                    );
            var administratorService = new AdministratorService(
                new InFileDictionaryRepository("path"),
                administrator);
            var user = new User(
                    Guid.NewGuid(),
                    "user",
                    new List<Word>(),
                    new Dictionary<Word, int>()
                    );

            administratorService.AddNewWord(user.Id, "word", "translation");
        }
    }
}
