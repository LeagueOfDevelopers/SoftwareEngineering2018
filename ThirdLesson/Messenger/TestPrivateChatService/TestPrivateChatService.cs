using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messenger;
using static MethodsForTests.MethodsForTests;

namespace TestPrivateChatService
{
    [TestClass]
    public class TestPrivateChatService
    {
        [TestMethod]
        public void AddMessage_NewMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var privateChat = CreatePrivateChat(userRepository, messageRepository);
            var privateChatService = CreatePrivateChatService(
                CreatePrivateChatRepository(privateChat));

            privateChatService.AddMessage(privateChat.Id, user1.Id, message);

            Assert.AreEqual(messageRepository.Items.First(), message);
        }

        [TestMethod]
        public void ChangeMessage_ChangedMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var privateChat = CreatePrivateChat(userRepository, messageRepository);
            var privateChatService = CreatePrivateChatService(
                CreatePrivateChatRepository(privateChat));

            var newBody = "Some test";
            privateChatService.ChangeMessage(privateChat.Id, user1.Id, message.Id, newBody);

            Assert.AreEqual(messageRepository.Items.First().Body, newBody);
        }

        [TestMethod]
        public void DeleteMessage_NoMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var privateChat = CreatePrivateChat(userRepository, messageRepository);
            var privateChatService = CreatePrivateChatService(
                CreatePrivateChatRepository(privateChat));

            privateChatService.DeleteMessage(privateChat.Id, user1.Id, message.Id);

            Assert.AreEqual(messageRepository.Items.Count(), 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteNotExistingMessage_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var privateChat = CreatePrivateChat(userRepository, messageRepository);
            var privateChatService = CreatePrivateChatService(new PrivateChatRepository(
                new List<PrivateChat> { privateChat }));

            var message = CreateMessageFrom(user1.Id);
            privateChatService.DeleteMessage(privateChat.Id, user1.Id, message.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void AddSameMessagesToPrivateChat_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var privateChat = CreatePrivateChat(userRepository, messageRepository);
            var privateChatService = CreatePrivateChatService(new PrivateChatRepository(
                new List<PrivateChat> { privateChat }));

            var message = CreateMessageFrom(user1.Id);
            privateChatService.AddMessage(privateChat.Id, user1.Id, message);
            privateChatService.AddMessage(privateChat.Id, user1.Id, message);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void DeleteForeignMessage_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var privateChat = CreatePrivateChat(userRepository, messageRepository);
            var privateChatService = CreatePrivateChatService(new PrivateChatRepository(
                new List<PrivateChat> { privateChat }));

            privateChatService.DeleteMessage(privateChat.Id, user2.Id, message.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void UpdateForeignMessage_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var privateChat = CreatePrivateChat(userRepository, messageRepository);
            var privateChatService = CreatePrivateChatService(new PrivateChatRepository(
                new List<PrivateChat> { privateChat }));

            var newBody = "New sample text";
            privateChatService.ChangeMessage(privateChat.Id, user2.Id, message.Id, newBody);
        }

        private PrivateChatRepository CreatePrivateChatRepository(PrivateChat privateChat)
        {
            return new PrivateChatRepository(new List<PrivateChat> { privateChat });
        }

        private PrivateChatService CreatePrivateChatService(PrivateChatRepository privateChatRepository)
        {
            return new PrivateChatService(Guid.NewGuid(), privateChatRepository);
        }
    }
}
