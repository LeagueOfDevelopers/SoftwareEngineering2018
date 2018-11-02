using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messenger;
using static MethodsForTests.MethodsForTests;

namespace TestChannel
{
    [TestClass]
    public class TestChannel
    {
        [TestMethod]
        public void AddMessageToChannelByAdmin_ChannelWithNewMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var messageRepository = CreateEmptyMessageRepository();
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);

            var message = CreateMessageFrom(user1.Id);
            channel.AddMessage(message);

            Assert.AreEqual(message.Id, channel.Messages.First().Id);
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
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);

            var newBody = "New sample text";
            channel.ChangeMessage(message.Id, newBody);

            Assert.AreEqual(message.Body, newBody);
        }

        [TestMethod]
        public void DeleteMessage_ChangedMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);

            channel.DeleteMessage(message.Id);

            Assert.AreEqual(channel.Messages.Count(), 0);
        }
    }
}
