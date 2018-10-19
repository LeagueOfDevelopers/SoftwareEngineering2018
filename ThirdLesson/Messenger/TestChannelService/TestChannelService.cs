using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messenger;
using static MethodsForTests.MethodsForTests;

namespace TestChannelService
{
    [TestClass]
    public class TestChannelService
    {
        [TestMethod]
        public void AddMessageByAdmin_NewMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);
            var channelRepository = CreateChannelService(CreateChannelRepository(channel));

            var message = CreateMessageFrom(user1.Id);
            channelRepository.AddMessage(channel.Id, user1.Id, message);

            Assert.AreEqual(message.Id, channel.Messages.First().Id);
        }

        [TestMethod]
        public void ChangeMessageByAdmin_ChangedMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);
            var channelRepository = CreateChannelService(CreateChannelRepository(channel));

            var newBody = "new TES";
            channelRepository.ChangeMessage(channel.Id, user1.Id, message.Id, newBody);

            Assert.AreEqual(newBody, channel.Messages.First().Body);
        }

        [TestMethod]
        public void DeleteMessageByAdmin_NoMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);
            var channelRepository = CreateChannelService(CreateChannelRepository(channel));

            channelRepository.DeleteMessage(channel.Id, user1.Id, message.Id);

            Assert.AreEqual(0, channel.Messages.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void AddSameMessagesToChannel_ChatWithNewMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);
            var channelRepository = CreateChannelService(CreateChannelRepository(channel));

            var message = CreateMessageFrom(user1.Id);
            channelRepository.AddMessage(channel.Id, user1.Id, message);
            channelRepository.AddMessage(channel.Id, user1.Id, message);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void AddMessageToChannelByUser_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);
            var channelRepository = CreateChannelService(CreateChannelRepository(channel));

            channelRepository.AddMessage(channel.Id, user2.Id, message);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void ChangeMessageByUser_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);
            var channelRepository = CreateChannelService(CreateChannelRepository(channel));

            var newBody = "New sample text";
            channelRepository.ChangeMessage(channel.Id, user2.Id, message.Id, newBody);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void DeleteMessageByUser_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);
            var channelRepository = CreateChannelService(CreateChannelRepository(channel));

            channelRepository.DeleteMessage(channel.Id, user2.Id, message.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteNotExistingMessage_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var channel = CreateChannel(userRepository, messageRepository, adminRepository);
            var channelRepository = CreateChannelService(CreateChannelRepository(channel));

            var message = CreateMessageFrom(user1.Id);
            channelRepository.DeleteMessage(channel.Id, user1.Id, message.Id);
        }

        private ChannelRepository CreateChannelRepository(Channel channel)
        {
            return new ChannelRepository(new List<Channel> { channel });
        }

        private ChannelService CreateChannelService(ChannelRepository channelRepository)
        {
            return new ChannelService(Guid.NewGuid(), channelRepository);
        }
    }
}
