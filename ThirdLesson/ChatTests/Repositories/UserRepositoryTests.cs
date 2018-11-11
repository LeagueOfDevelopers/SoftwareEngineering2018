using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System.Collections.Generic;

namespace ChatTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void AddUserInRepa_UserWillBeAdded()
        {
            StubUser user = new StubUser(Guid.NewGuid());
            UserRepository repa = new UserRepository(new List<IUser>());

            repa.AddUser(user);
            
            foreach (IUser UserInRepa in repa.Users)
            {
                Assert.IsTrue(user.Id == UserInRepa.Id);
            }
        }

        [TestMethod]
        public void CallContainsWhenRepaContainsUser_returnTrue()
        {
            StubUser user = new StubUser(Guid.NewGuid());
            UserRepository repa = new UserRepository(new List<IUser>(new IUser[] { user }));
            bool expectedAnsw = true;

            bool answ = repa.Contains(user.Id);

            Assert.IsTrue(answ == expectedAnsw);
        }
    }
}
