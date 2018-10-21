using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System.Collections.Generic;
namespace ChatTests
{
    [TestClass]
    public class PrivateChatTests
    {
        [TestMethod]
        public void AddMessage_MessageWillBeAppend()
        {
            StubUser user = new StubUser(Guid.NewGuid());
            StubUser[] users = new StubUser[] { user };
            PrivateChat chat = new PrivateChat(
                Guid.NewGuid(),
                new List<Message>(),
                new List<IUser>(users));
            Message message = new Message(Guid.NewGuid(), user, "hello");

            chat.AddMessage(message);

            foreach(Message mes in chat.Messages)
            {
                Assert.IsTrue(mes.Equals(message));
            }
        }

        [TestMethod]
        public void DeleteMessage_MessageWillBeRemoved()
        {
            StubUser user = new StubUser(Guid.NewGuid());
            StubUser[] users = new StubUser[] { user };
            Message message = new Message(Guid.NewGuid(), user, "hello");
            PrivateChat chat = new PrivateChat(
                Guid.NewGuid(),
                new List<Message>(new Message[] { message}),
                new List<IUser>(users));

            chat.DeleteMessage(message.Id, user);

            foreach (Message mes in chat.Messages)
            {
                Assert.IsFalse(mes.Equals(message));
            }
        }

        [TestMethod]
        public void EditMessage_MessageChangeText()
        {
            StubUser user = new StubUser(Guid.NewGuid());
            StubUser[] users = new StubUser[] { user };
            Message message = new Message(Guid.NewGuid(), user, "hello");
            PrivateChat chat = new PrivateChat(
                Guid.NewGuid(),
                new List<Message>(new Message[] { message }),
                new List<IUser>(users));
            string newText = "NewHello";

            chat.EditMessage(message.Id, user, newText);

            foreach(Message mes in chat.Messages)
            {
                Assert.IsTrue(mes.Text == newText);
            }
        }
    }
}
