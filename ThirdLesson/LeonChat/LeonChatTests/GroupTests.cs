using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LeonChat;


namespace LeonChatTests
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public void AddMessage_NewTextAppropriate()
        {
            Guid adminId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId, userId};
            Message[] messages = Array.Empty<Message>();
            Group group = new Group(adminsIds, usersIds, messages);
            string msgText = "Oh, Hello";

            group.AddMessage(usersIds[0], msgText);
            List<Message> returnedMessages = (List<Message>) group.GetMessages(usersIds[0]);
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
            Group group = new Group(adminsIds, usersIds, messages);
            string msgText = "Oh, Hello";
            Guid unknownGuid = Guid.NewGuid();

            Assert.ThrowsException<Exception>(() => group.AddMessage(unknownGuid, msgText));
        }

        [TestMethod]
        public void AddParticipant_UserAdding_ParticipantsAppended()
        {
            Guid adminId = Guid.NewGuid();
            Guid newUserId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] messages = Array.Empty<Message>();
            List<Guid> expectedIds = new List<Guid> {adminId, newUserId};

            Group group = new Group(adminsIds, usersIds, messages);

            group.AddParticipant(adminId, newUserId);

            List<Guid> returnedIds = (List<Guid>) group.GetParticipants(adminId);

            CollectionAssert.AreEquivalent(expectedIds, returnedIds);
        }

        [TestMethod]
        public void GetMessages_InitialiseWith_MessagesReturned()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] expectedMessages = {new Message(adminId, 0, "hello")};

            Group group = new Group(adminsIds, usersIds, expectedMessages);

            List<Message> returnedMessages = (List<Message>) group.GetMessages(adminId);

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

            Group group = new Group(adminsIds, usersIds, messages);

            group.DeleteMessage(adminId, 0);

            List<Message> returnedMessages = (List<Message>) group.GetMessages(adminId);

            CollectionAssert.AreEquivalent(expetced, returnedMessages);
        }

        [TestMethod]
        public void DeleteMessage_InappropriateMessage_Exception()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] expectedMessages = {new Message(adminId, 0, "hello")};

            Group group = new Group(adminsIds, usersIds, expectedMessages);

            Assert.ThrowsException<Exception>(() => group.DeleteMessage(adminId, 999));
        }

        [TestMethod]
        public void EditMessage_UserOnSelf_MessageEdited()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid[] usersIds = {adminId};
            Message[] messages = {new Message(adminId, 0, "Not edited")};
            Message[] expected = {new Message(adminId, 0, "Edited")};

            Group group = new Group(adminsIds, usersIds, messages);

            group.EditMessage(adminId, 0, "Edited");

            List<Message> returnedMessages = (List<Message>) group.GetMessages(adminId);

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

            Group group = new Group(adminsIds, participantsIds, messages);

            Assert.ThrowsException<Exception>(() => @group.EditMessage(userId, 0, "Edited"));
        }

        [TestMethod]
        public void RemoveParticipant_UserOnSelf_Removed()
        {
            Guid adminId = Guid.NewGuid();
            Guid[] adminsIds = {adminId};
            Guid userId = Guid.NewGuid();
            Guid[] participantsIds = {adminId, userId};
            Message[] messages = Array.Empty<Message>();

            Group group = new Group(adminsIds, participantsIds, messages);
            
            group.RemoveParticipant(userId, userId);

            List<Guid> returnedParticipants = (List<Guid>)group.GetParticipants(adminId);
            
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

            Group group = new Group(adminsIds, participantsIds, messages);
            
            group.RemoveParticipant(adminId, userId);

            List<Guid> returnedParticipants = (List<Guid>)group.GetParticipants(adminId);
            
            CollectionAssert.AreEquivalent(adminsIds, returnedParticipants);
        }
    }
}