using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messenger;
using static MethodsForTests.MethodsForTests;

namespace TestGroup
{
    [TestClass]
    public class TestGroup
    {
        [TestMethod]
        public void AddMessageToGriup_GroupWithNewMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var messageRepository = CreateEmptyMessageRepository();
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);

            var message = CreateMessageFrom(user1.Id);
            group.AddMessage(message);

            Assert.AreEqual(message.Id, group.Messages.First().Id);
        }

        [TestMethod]
        public void DeleteMessage_PrivateChatWithNoMessages()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);

            group.DeleteMessage(message.Id);

            Assert.AreEqual(group.Messages.Count(), 0);
        }

        [TestMethod]
        public void ChangeMessage_ChangedMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);

            var newBody = "New sample text";
            group.ChangeMessage(message.Id, newBody);

            Assert.AreEqual(message.Body, newBody);
        }
    }
}
