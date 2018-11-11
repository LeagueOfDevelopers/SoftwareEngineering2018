using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System.Collections.Generic;

namespace ChatTests
{
    [TestClass]
    public class GroupChatTests
    {
        [TestMethod]
        public void AddMessageByAdmin_MessageWillAppend()
        {
            StubUser admin = new StubUser(Guid.NewGuid());
            StubUser[] admins = new StubUser[] { admin };
            StubUser user = new StubUser(Guid.NewGuid());
            StubUser[] users = new StubUser[] { user };
            GroupChat chat = new GroupChat(
                Guid.NewGuid(),
                new List<Message>(),
                new List<IUser>(users),
                new List<IUser>(admins));
            Message message = new Message(Guid.NewGuid(), admin, "hello");

            chat.AddMessage(message);

            foreach (Message mes in chat.Messages)
            {
                Assert.IsTrue(mes.Equals(message));
            }
        }

        [TestMethod]
        public void AddMessageByNotAdmin_returnException()
        {
            StubUser admin = new StubUser(Guid.NewGuid());
            StubUser[] admins = new StubUser[] { admin };
            StubUser user = new StubUser(Guid.NewGuid());
            StubUser[] users = new StubUser[] { user };
            GroupChat chat = new GroupChat(
                Guid.NewGuid(),
                new List<Message>(),
                new List<IUser>(users),
                new List<IUser>(admins));
            Message message = new Message(Guid.NewGuid(), user, "hello");

            try
            {
                chat.AddMessage(message);
                Assert.Fail();
            }
            catch (Exception e)
            {
                string exceptMessage = "Try to send a message by not a Admin";
                Assert.IsTrue(e.Message == exceptMessage);
            }
        }

        [TestMethod]
        public void DeleteMessageByAdmin_MessageRemove()
        {
            StubUser admin = new StubUser(Guid.NewGuid());
            StubUser[] admins = new StubUser[] { admin };
            StubUser user = new StubUser(Guid.NewGuid());
            StubUser[] users = new StubUser[] { user };
            GroupChat chat = new GroupChat(
                Guid.NewGuid(),
                new List<Message>(),
                new List<IUser>(users),
                new List<IUser>(admins));
            Message message = new Message(Guid.NewGuid(), admin, "hello");

            chat.DeleteMessage(message.Id, admin);

            foreach (Message mes in chat.Messages)
            {
                Assert.IsFalse(mes.Equals(message));
            }
        }

        [TestMethod]
        public void EditMessageByAdmin_MessageChangeText()
        {
            StubUser admin = new StubUser(Guid.NewGuid());
            StubUser[] admins = new StubUser[] { admin };
            StubUser user = new StubUser(Guid.NewGuid());
            StubUser[] users = new StubUser[] { user };
            GroupChat chat = new GroupChat(
                Guid.NewGuid(),
                new List<Message>(),
                new List<IUser>(users),
                new List<IUser>(admins));
            Message message = new Message(Guid.NewGuid(), user, "hello");
            string newText = "NewHello";

            chat.EditMessage(message.Id, admin, newText);

            foreach (Message mes in chat.Messages)
            {
                Assert.IsTrue(mes.Text == newText);
            }
        }
    }
}
