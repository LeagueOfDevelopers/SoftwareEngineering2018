using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LoDChat.Tests
{
    [TestClass]
    public class PrivateChatTests
    {
        [TestMethod]
        public void CheckIfUserInChat_ReturnTrue()
        {
            List<Message> messages = new List<Message>();
            List<IUser> users = new List<IUser>();
            Guid idOfChat = Guid.NewGuid();
            PrivateChat privateChat = new PrivateChat(idOfChat, messages, users);
            List<IChat> chats = new List<IChat> { privateChat };
            User user = new User("UserName", Guid.NewGuid(), chats);

            bool check = privateChat.IsBeInChat(user);

            Assert.IsTrue(check);
        }
        [TestMethod]
        public void CheckIsUserOwnMessage_ReturnTrue()
        {
            Guid equalId = Guid.NewGuid();
            Message message = new Message("Hello World", equalId, Guid.NewGuid());
            List<IChat> chats = new List<IChat>();
            List<Message> messages = new List<Message> { message };
            User user = new User("UserName", equalId, chats);
            List<IUser> users = new List<IUser> { user };
            PrivateChat privateChat = new PrivateChat(Guid.NewGuid(), messages, users);
            user.Chats.Add(privateChat);

            bool check = privateChat.IsOwnerOfMessage(equalId,user);

            Assert.IsTrue(check);
        }
        [TestMethod]
        public void AddNewMessage_ReturnTrueIfChatContainsNewMessage()
        {
            List<IChat> chats = new List<IChat>();
            List<Message> messages = new List<Message>();
            User user = new User("UserName", Guid.NewGuid(), chats);
            List<IUser> users = new List<IUser> { user };
            PrivateChat privateChat = new PrivateChat(Guid.NewGuid(), messages, users);
            user.Chats.Add(privateChat);
            bool check = false;

            privateChat.AddMessage(user, "New Message For Test");
            foreach (var message in privateChat.Messages)
            {
                if(message.UserMessage == "New Message For Test")
                {
                    check = true;
                }
            }

            Assert.IsTrue(check);
        }
        [TestMethod]
        public void DeleteMessage_GetBlankListWithoutMessages()
        {
            Guid equalId = Guid.NewGuid();
            Guid idOfMessage = Guid.NewGuid();
            List<IChat> chats = new List<IChat>();
            Message message = new Message("Simple Message", equalId, idOfMessage);
            List<Message> messages = new List<Message> { message };
            User user = new User("UserName", equalId, chats);
            List<IUser> users = new List<IUser> { user };
            PrivateChat privateChat = new PrivateChat(Guid.NewGuid(), messages, users);
            user.Chats.Add(privateChat);
            List<Message> expectedMessages = new List<Message>();

            privateChat.DeleteMessage(user, idOfMessage);

            CollectionAssert.AreEqual((List<Message>)privateChat.Messages, expectedMessages);
        }
        [TestMethod]
        public void EditMessage_GetEditedMessage()
        {
            Guid equalId = Guid.NewGuid();
            Guid idOfMessage = Guid.NewGuid();
            List<IChat> chats = new List<IChat>();
            Message message = new Message("Simple Message", equalId, idOfMessage);
            List<Message> messages = new List<Message> { message };
            User user = new User("UserName", equalId, chats);
            List<IUser> users = new List<IUser> { user };
            PrivateChat privateChat = new PrivateChat(Guid.NewGuid(), messages, users);
            user.Chats.Add(privateChat);
            var editedMessageText = "Henlo";

            privateChat.EditMessage(user, idOfMessage, editedMessageText);

            Assert.AreEqual(message.UserMessage, editedMessageText);
        }
        public IUser CreateSimpleUser()
        {
            List<Message> messages = new List<Message>();
            List<IUser> users = new List<IUser>();
            PrivateChat privateChat = new PrivateChat(Guid.NewGuid(), messages, users);
            List<IChat> chats = new List<IChat> { privateChat };
            User user = new User("UserName", Guid.NewGuid(), chats);
            return user;

        }
        
    }
}