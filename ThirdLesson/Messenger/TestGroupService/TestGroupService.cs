using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messenger;
using static MethodsForTests.MethodsForTests;

namespace TestGroupService
{
    [TestClass]
    public class TestGroupService
    {
        [TestMethod]
        public void AddMessage_NewMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user1.Id);
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            groupService.AddMessage(group.Id, user1.Id, message);

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
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            var newBody = "New sample text";
            groupService.ChangeMessage(group.Id, user1.Id, message.Id, newBody);

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
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            groupService.DeleteMessage(group.Id, user1.Id, message.Id);

            Assert.AreEqual(messageRepository.Items.Count(), 0);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void AddSameMessagesToGroup_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var messageRepository = CreateEmptyMessageRepository();
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            var message = CreateMessageFrom(user1.Id);
            groupService.AddMessage(group.Id, user1.Id, message);
            groupService.AddMessage(group.Id, user1.Id, message);
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
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            var message = CreateMessageFrom(user1.Id);
            groupService.DeleteMessage(group.Id, user1.Id, message.Id);
        }

        [TestMethod]
        public void DeleteForeignMessageByAdmin_NoMessage()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user2.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            groupService.DeleteMessage(group.Id, user1.Id, message.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void UpdateForeignMessageByAdmin_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user2.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            var newBody = "New sample text";
            groupService.ChangeMessage(group.Id, user1.Id, message.Id, newBody);
        }

        [TestMethod]
        public void DeleteForeignMessageByUser_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user2.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            groupService.DeleteMessage(group.Id, user1.Id, message.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(MemberAccessException))]
        public void UpdateForeignMessageByUser_Exception()
        {
            var user1 = CreateUser();
            var user2 = CreateUser();
            var message = CreateMessageFrom(user2.Id);
            var messageRepository = CreateMessageRepositoryWithOneMessage(message);
            var userRepository = CreateUserRepositoryForTwo(user1, user2);
            var adminRepository = CreateUserRepositoryWithAdmin(user1);
            var group = CreateGroup(userRepository, messageRepository, adminRepository);
            var groupService = CreateGroupService(CreateGroupRepository(group));

            var newBody = "sOme";
            groupService.ChangeMessage(group.Id, user1.Id, message.Id, newBody);
        }

        private GroupRepository CreateGroupRepository(Group group)
        {
            return new GroupRepository(new List<Group> { group });
        }

        private GroupService CreateGroupService(GroupRepository groupRepository)
        {
            return new GroupService(Guid.NewGuid(), groupRepository);
        }
    }
}
