using System;
using System.Collections.Generic;
using LoDMessenger;
using LoDMessenger.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoDMessengerTests
{
    [TestClass]
    public class PrivateChatTests
    {
        [TestMethod]
        public void GetMessagesTest_GiveTrueParticipant_GetMessages()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var companionId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var companion = new User(
                companionId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var participants = new IUser[] { creator, companion };
            var privatChatId = Guid.NewGuid();
            var privateChat = new PrivateChat(
                privatChatId,
                participants,
                messages);

            var getMessagesResult = privateChat.GetMessages(creator);

            CollectionAssert.AreEqual((List<IMessage>)getMessagesResult, messages);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetMessagesTest_GiveFakeParticipant_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var companionId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var companion = new User(
                companionId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var participants = new IUser[] { creator, companion };
            var privatChatId = Guid.NewGuid();
            var privateChat = new PrivateChat(
                privatChatId,
                participants,
                messages);
            var fakeGetter = new User(
                Guid.NewGuid(),
                "Vasya",
                emptyChats);

            privateChat.GetMessages(fakeGetter);
        }
        [TestMethod]
        public void GetParticipantsTest_GiveTrueParticipant_GetParticipants()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var companionId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var companion = new User(
                companionId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var participants = new IUser[] { creator, companion };
            var privatChatId = Guid.NewGuid();
            var privateChat = new PrivateChat(
                privatChatId,
                participants,
                messages);

            var getParticipantsResult = privateChat.GetParticipants(creator);

            CollectionAssert.AreEqual((IUser[])getParticipantsResult, participants);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetParticipantsTest_GiveFakeParticipant_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var companionId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var companion = new User(
                companionId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var participants = new IUser[] { creator, companion };
            var privatChatId = Guid.NewGuid();
            var privateChat = new PrivateChat(
                privatChatId,
                participants,
                messages);
            var fakeGetter = new User(
                Guid.NewGuid(),
                "Vasya",
                emptyChats);

            privateChat.GetParticipants(fakeGetter);
        }
        [TestMethod]
        public void EditMessageTest_GiveTrueEditorAndMessageId_MessageIsEdit()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var companionId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var companion = new User(
                companionId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var participants = new IUser[] { creator, companion };
            var privatChatId = Guid.NewGuid();
            var privateChat = new PrivateChat(
                privatChatId,
                participants,
                messages);
            var editedText = "EditedText";

            privateChat.EditMessage(creator, messageId, editedText);

            Assert.AreEqual(editedText, message.MessageText);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditMessageTest_GiveFakeEditor_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var companionId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var companion = new User(
                companionId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var participants = new IUser[] { creator, companion };
            var privatChatId = Guid.NewGuid();
            var privateChat = new PrivateChat(
                privatChatId,
                participants,
                messages);
            var editedText = "EditedText";
            var fakeEditor = new User(
                Guid.NewGuid(),
                "Vasya",
                emptyChats);

            privateChat.EditMessage(fakeEditor, messageId, editedText);
        }

        [TestMethod]
        public void SendMessageTest_GiveTrueSender_MessageIsSend()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var companionId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var companion = new User(
                companionId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>();
            var participants = new IUser[] { creator, companion };
            var privatChatId = Guid.NewGuid();
            var privateChat = new PrivateChat(
                privatChatId,
                participants,
                messages);

            privateChat.SendMessage(creator, message);

            CollectionAssert.Contains(messages, message);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SendMessageTest_GiveFakeSender_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var companionId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var companion = new User(
                companionId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>();
            var participants = new IUser[] { creator, companion };
            var privatChatId = Guid.NewGuid();
            var privateChat = new PrivateChat(
                privatChatId,
                participants,
                messages);
            var fakeSender = new User(
               Guid.NewGuid(),
               "BadBoy",
               emptyChats);

            privateChat.SendMessage(fakeSender, message);
        }
    }
}
