using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LeonChat;

namespace LeonChatTests
{
    [TestClass]
    public class ChannelTests
    {
        [TestMethod]
        public void AddMessage_AdminAdded_NewTextAppropriate()
        {
            Guid adminId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId, userId};
            Message[] messages = Array.Empty<Message>();
            string msgText = "Oh, Hello";

            Channel channel = new Channel(adminsIds, usersIds, messages);

            channel.AddMessage(usersIds[0], msgText);
            
            List<Message> returnedMessages = (List<Message>) channel.GetMessages(usersIds[0]);
            Message returned = returnedMessages[0];

            Assert.AreEqual(msgText, returned.Text);
        }

        [TestMethod]
        public void AddMessage_UserUnknown_Forbidden()
        {
            Guid adminId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId, userId};
            Message[] messages = Array.Empty<Message>();
            Channel channel = new Channel(adminsIds, usersIds, messages);
            string msgText = "Oh, Hello";
            Guid unknownGuid = Guid.NewGuid();

            Assert.ThrowsException<Exception>(() => channel.AddMessage(unknownGuid, msgText));
        }

        [TestMethod]
        public void AddParticipant_UserAddingSelf_ParticipantsAppended()
        {
            Guid adminId = Guid.NewGuid();
            Guid newUserId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] messages = Array.Empty<Message>();
            List<Guid> expectedIds = new List<Guid> {adminId, newUserId};

            Channel channel = new Channel(adminsIds, usersIds, messages);

            channel.AddParticipant(newUserId, newUserId);

            List<Guid> returnedIds = (List<Guid>) channel.GetParticipants(adminId);

            CollectionAssert.AreEquivalent(expectedIds, returnedIds);
        }
        
        [TestMethod]
        public void AddParticipant_UserAddingAnother_ParticipantsAppended()
        {
            Guid adminId = Guid.NewGuid();
            Guid newUserId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] messages = Array.Empty<Message>();
            List<Guid> expectedIds = new List<Guid> {adminId, newUserId};

            Channel channel = new Channel(adminsIds, usersIds, messages);

            Assert.ThrowsException<Exception>(() => channel.AddParticipant(adminId, newUserId));
        }

        [TestMethod]
        public void GetMessages_InitialiseWith_MessagesReturned()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] expectedMessages = {new Message(adminId, 0, "hello")};

            Channel channel = new Channel(adminsIds, usersIds, expectedMessages);

            List<Message> returnedMessages = (List<Message>) channel.GetMessages(adminId);

            CollectionAssert.AreEquivalent(expectedMessages, returnedMessages);
        }

        [TestMethod]
        public void DeleteMessage_InitialiseWith_EmptyMessagesReturned()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] messages = {new Message(adminId, 0, "hello")};
            Message[] expetced = Array.Empty<Message>();

            Channel channel = new Channel(adminsIds, usersIds, messages);

            channel.DeleteMessage(adminId, 0);

            List<Message> returnedMessages = (List<Message>) channel.GetMessages(adminId);

            CollectionAssert.AreEquivalent(expetced, returnedMessages);
        }

        [TestMethod]
        public void DeleteMessage_InappropriateMessage_Exception()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] expectedMessages = {new Message(adminId, 0, "hello")};

            Channel channel = new Channel(adminsIds, usersIds, expectedMessages);

            Assert.ThrowsException<Exception>(() => channel.DeleteMessage(adminId, 999));
        }

        [TestMethod]
        public void EditMessage_UserOnSelf_MessageEdited()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] messages = {new Message(adminId, 0, "Not edited")};
            Message[] expected = {new Message(adminId, 0, "Edited")};

            Channel channel = new Channel(adminsIds, usersIds, messages);

            channel.EditMessage(adminId, 0, "Edited");

            List<Message> returnedMessages = (List<Message>) channel.GetMessages(adminId);

            Assert.AreEqual("Edited", returnedMessages[0].Text);
        }

        [TestMethod]
        public void EditMessage_UserOnAnother_Exception()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid userId = Guid.NewGuid();
            Guid[] participantsIds = {adminId, userId};
            Message[] messages = {new Message(adminId, 0, "Not edited")};

            Channel channel = new Channel(adminsIds, participantsIds, messages);

            Assert.ThrowsException<Exception>(() => channel.EditMessage(userId, 0, "Edited"));
        }

        [TestMethod]
        public void RemoveParticipant_UserOnSelf_Removed()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid userId = Guid.NewGuid();
            Guid[] participantsIds = {adminId, userId};
            Message[] messages = Array.Empty<Message>();

            Channel channel = new Channel(adminsIds, participantsIds, messages);

            channel.RemoveParticipant(userId, userId);

            List<Guid> returnedParticipants = (List<Guid>) channel.GetParticipants(adminId);

            CollectionAssert.AreEquivalent(adminsIds, returnedParticipants);
        }

        [TestMethod]
        public void RemoveParticipant_AdminOnAnother_Removed()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid userId = Guid.NewGuid();
            Guid[] participantsIds = {adminId, userId};
            Message[] messages = Array.Empty<Message>();

            Channel channel = new Channel(adminsIds, participantsIds, messages);

            channel.RemoveParticipant(adminId, userId);

            List<Guid> returnedParticipants = (List<Guid>) channel.GetParticipants(adminId);

            CollectionAssert.AreEquivalent(adminsIds, returnedParticipants);
        }
    }
}