using System;
using System.Collections.Generic;
using Messenger;

namespace MethodsForTests
{
    public static class MethodsForTests
    {
        public static Message CreateMessageFrom(Guid userId)
        {
            return new Message(
                Guid.NewGuid(),
                userId,
                "Sample message");
        }

        public static User CreateUser()
        {
            return new User(
                Guid.NewGuid(),
                "Sample name");
        }

        public static PrivateChat CreatePrivateChat(
            UserRepository userRepository,
            MessageRepository messageRepository
            )
        {
            return new PrivateChat(
                Guid.NewGuid(),
                userRepository,
                messageRepository);
        }

        public static Group CreateGroup(
            UserRepository userRepository,
            MessageRepository messageRepository,
            UserRepository adminRepository)
        {
            return new Group(
                Guid.NewGuid(),
                "Sample name",
                userRepository,
                messageRepository,
                adminRepository);
        }

        public static Channel CreateChannel(
           UserRepository userRepository,
           MessageRepository messageRepository,
           UserRepository adminRepository)
        {
            return new Channel(
                Guid.NewGuid(),
                "Sample name",
                userRepository,
                messageRepository,
                adminRepository);
        }

        public static UserRepository CreateUserRepositoryForTwo(User user1, User user2)
        {
            return new UserRepository(new List<User> { user1, user2 });
        }

        public static MessageRepository CreateMessageRepositoryWithOneMessage(Message message)
        {
            return new MessageRepository(new List<Message> { message });
        }

        public static MessageRepository CreateEmptyMessageRepository()
        {
            return new MessageRepository(new List<Message>());
        }

        public static UserRepository CreateUserRepositoryWithAdmin(User admin)
        {
            return new UserRepository(new List<User> { admin });
        }
    }
}
