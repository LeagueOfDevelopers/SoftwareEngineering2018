using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messenger;
using static MethodsForTests.MethodsForTests;

namespace TestPrivateChat
{
    [TestClass]
    public class TestPrivateChat
    {
        [TestMethod]
        public void AddMessageToPrivateChat_ChatWithNewMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var messageRepository = new MessageRepository(new List<Message>());
            var userRepository = new UserRepository(new List<User> { user1, user2 });
            var privateChat = CreatePrivateChat(userRepository, messageRepository);

            var message = CreateMessageFrom(user1.Id);
            privateChat.AddMessage(message);

            Assert.AreEqual(message.Id, privateChat.Messages.First().Id);
        }

        [TestMethod]
        public void ChangeMessageInPrivateChat_ChangedMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = new MessageRepository(new List<Message> { message });
            var userRepository = new UserRepository(new List<User> { user1, user2 });
            var privateChat = CreatePrivateChat(userRepository, messageRepository);

            var newBody = "New sample";
            privateChat.ChangeMessage(message.Id, newBody);

            Assert.AreEqual(message.Body, newBody);
        }

        [TestMethod]
        public void DeleteMessageInPrivateChat_NoMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = new MessageRepository(new List<Message> { message });
            var userRepository = new UserRepository(new List<User> { user1, user2 });
            var privateChat = CreatePrivateChat(userRepository, messageRepository);

            privateChat.DeleteMessage(message.Id);

            Assert.AreEqual(privateChat.Messages.Count(), 0);
        }
    }
}
