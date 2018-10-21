using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LeonChat;


namespace LeonChatTests
{
    [TestClass]
    public class PersonalChatTests
    {
        [TestMethod]
        public void AddMessage_NewTextAppropriate()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            Message[] messages = Array.Empty<Message>();
            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, messages);
            string msgText = "Oh, Hello";

            personalChat.AddMessage(peerOne, msgText);

            List<Message> returnedMessages = (List<Message>) personalChat.GetMessages(peerTwo);
            Message returned = returnedMessages[0];

            Assert.AreEqual(msgText, returned.Text);
        }

        [TestMethod]
        public void AddMessage_UserUnknown_Forbidden()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            Message[] messages = Array.Empty<Message>();
            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, messages);
            Guid unknownGuid = Guid.NewGuid();
            string newMessageText = "test text";

            Assert.ThrowsException<Exception>(() => personalChat.AddMessage(unknownGuid, newMessageText));
        }

        [TestMethod]
        public void AddParticipant_UserAdding_ParticipantsAppended()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            Message[] messages = Array.Empty<Message>();
            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, messages);
            Guid newUserId = Guid.NewGuid();

            Assert.ThrowsException<NotImplementedException>(() => personalChat.AddParticipant(peerOne, newUserId));
        }

        [TestMethod]
        public void GetMessages_InitialiseWith_MessagesReturned()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            //Message[] messages = Array.Empty<Message>();
            Message[] expectedMessages = {new Message(peerOne, 0, "hello")};
            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, expectedMessages);

            List<Message> returnedMessages = (List<Message>) personalChat.GetMessages(peerTwo);

            CollectionAssert.AreEquivalent(expectedMessages, returnedMessages);
        }

        [TestMethod]
        public void DeleteMessage_InitialiseWith_EmptyMessagesReturned()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            Message[] messages = {new Message(peerOne, 0, "hello")};
            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, messages);

            Message[] expetced = Array.Empty<Message>();

            personalChat.DeleteMessage(peerOne, 0);

            List<Message> returnedMessages = (List<Message>) personalChat.GetMessages(peerTwo);

            CollectionAssert.AreEquivalent(expetced, returnedMessages);
        }

        [TestMethod]
        public void DeleteMessage_InappropriateMessage_Exception()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            Message[] messages = {new Message(peerOne, 0, "hello")};
            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, messages);


            Assert.ThrowsException<Exception>(() => personalChat.DeleteMessage(peerOne, 999));
        }

        [TestMethod]
        public void EditMessage_UserOnSelf_MessageEdited()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            Message[] messages = {new Message(peerOne, 0, "Not edited")};

            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, messages);

            personalChat.EditMessage(peerOne, 0, "Edited");

            List<Message> returnedMessages = (List<Message>) personalChat.GetMessages(peerTwo);

            Assert.AreEqual("Edited", returnedMessages[0].Text);
        }

        [TestMethod]
        public void EditMessage_UserOnAnother_Exception()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            Message[] messages = {new Message(peerOne, 0, "Not edited")};

            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, messages);

            Assert.ThrowsException<Exception>(() => personalChat.EditMessage(peerTwo, 0, "Edited"));
        }

        [TestMethod]
        public void RemoveParticipant_UserOnSelf_Removed()
        {
            Guid peerOne = Guid.NewGuid();
            Guid peerTwo = Guid.NewGuid();
            Message[] messages = Array.Empty<Message>();
            PersonalChat personalChat = new PersonalChat(peerOne, peerTwo, messages);

            Assert.ThrowsException<Exception>(() => personalChat.RemoveParticipant(peerOne, peerTwo));
        }
    }
}