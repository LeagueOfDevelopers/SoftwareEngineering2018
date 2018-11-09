using System;
using System.Collections.Generic;
using BusinessEntities;
using BusinessServices.Exceptions;
using BusinessServices.Services;
using Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class AdministratorServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(PermissionDeniedException))]
        public void AddDictionaryPairTest_NotAdminTryToAddNewWord_ThrowException()
        {
            var admin = new Administrator(Guid.NewGuid(), "admin");
            var administratorService = new AdministratorService(
                new TestDictionaryRepository(), admin);

            administratorService.AddDictionaryPair(Guid.NewGuid(), "empty", "empty");
        }

        [TestMethod]
        public void AddDictionaryPairTest_AdminTryToAddNewWord_SavingMethodWasCalled()
        {
            var adminId = Guid.NewGuid();
            var admin = new Administrator(adminId, "admin");
            var testRepository = new TestDictionaryRepository();
            var administratorService = new AdministratorService(
                testRepository, admin);

            administratorService
                .AddDictionaryPair(adminId, "empty", "empty");

            Assert.IsTrue(testRepository.WasCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(PermissionDeniedException))]
        public void DeleteDictionaryPairTest_NotAdminTryToDeleteWord_ThrowException()
        {
            var admin = new Administrator(Guid.NewGuid(), "admin");
            var administratorService = new AdministratorService(
                new TestDictionaryRepository(), admin);

            administratorService.DeleteDictionaryPair(Guid.NewGuid(), Guid.NewGuid());
        }

        [TestMethod]
        public void DeleteDictionaryPairTest_AdminTryToDeleteWord_RemovingMethodWasCalled()
        {
            var adminId = Guid.NewGuid();
            var admin = new Administrator(adminId, "admin");
            var testRepository = new TestDictionaryRepository();
            var administratorService = new AdministratorService(
                testRepository, admin);

            administratorService
                .DeleteDictionaryPair(adminId, Guid.Empty);

            Assert.IsTrue(testRepository.WasCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(PermissionDeniedException))]
        public void LoadDictionaryTest_NotAdminTryToLoadDictionary_ThrowException()
        {
            var admin = new Administrator(Guid.NewGuid(), "admin");
            var administratorService = new AdministratorService(
                new TestDictionaryRepository(), admin);

            administratorService.LoadDictionary(Guid.NewGuid());
        }

        [TestMethod]
        public void LoadDictionaryTest_AdminTryToLoadDictionary_LoadingMethodWasCalled()
        {
            var adminId = Guid.NewGuid();
            var admin = new Administrator(adminId, "admin");
            var testRepository = new TestDictionaryRepository();
            var administratorService = new AdministratorService(
                testRepository, admin);

            administratorService
                .LoadDictionary(adminId);

            Assert.IsTrue(testRepository.WasCalled);
        }

        private class TestDictionaryRepository : IDictionaryRepository
        {
            public bool WasCalled { get; private set; }

            public Word LoadRandomWord()
            {
                throw new NotImplementedException();
            }

            public Translation GetWordTranslation(Word word)
            {
                throw new NotImplementedException();
            }

            public void SaveDictionaryPair(DictionaryPair dictionaryPair)
            {
                WasCalled = true;
            }

            public void DeleteDictionaryPair(Guid pairId)
            {
                WasCalled = true;
            }

            public IEnumerable<DictionaryPair> LoadDictionary()
            {
                WasCalled = true;
                return null;
            }
        }
    }
}
