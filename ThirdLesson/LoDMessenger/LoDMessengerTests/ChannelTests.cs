using System;
using System.Collections.Generic;
using LoDMessenger;
using LoDMessenger.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoDMessengerTests
{
    [TestClass]
    public class ChannelTests
    {
        [TestMethod]
        public void GetMessagesTest_GiveTrueParticipant_GetMessages()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var subscriber = new User(
                subscriberId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var subscribers = new List<IUser>() { subscriber };
            var administrators = new List<IUser>() { creator };
            var channelId = Guid.NewGuid();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);

            var getMessagesResult = channel.GetMessages(subscriber);

            CollectionAssert.AreEqual((List<IMessage>)getMessagesResult, messages);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetMessagesTest_GiveFakeSubscriber_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var subscriber = new User(
                subscriberId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var subscribers = new List<IUser>() { subscriber };
            var administrators = new List<IUser>() { creator };
            var channelId = Guid.NewGuid();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);
            var fakeSubscriber = new User(
                Guid.NewGuid(),
                "Vasya",
                emptyChats);

            channel.GetMessages(fakeSubscriber);
        }
        [TestMethod]
        public void GetParticipantsTest_GiveTrueSubscriber_GetParticipants()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var subscriber = new User(
                subscriberId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var subscribers = new List<IUser>() { subscriber };
            var administrators = new List<IUser>() { creator };
            var channelId = Guid.NewGuid();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);

            var getParticipantsResult = channel.GetParticipants(subscriber);

            CollectionAssert.AreEqual((List<IUser>)getParticipantsResult, subscribers);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetParticipantsTest_GiveFakeSubscriber_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var subscriber = new User(
                subscriberId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var subscribers = new List<IUser>() { subscriber };
            var administrators = new List<IUser>() { creator };
            var channelId = Guid.NewGuid();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);
            var fakeSubscriber = new User(
                Guid.NewGuid(),
                "Vasya",
                emptyChats);

            channel.GetParticipants(fakeSubscriber);
        }
        [TestMethod]
        public void EditMessageTest_GiveTrueEditorAndMessageId_MessageIsEdit()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var subscriber = new User(
                subscriberId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var subscribers = new List<IUser>() { subscriber };
            var administrators = new List<IUser>() { creator };
            var channelId = Guid.NewGuid();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);
            var editedText = "EditedText";

            channel.EditMessage(creator, messageId, editedText);

            Assert.AreEqual(editedText, message.MessageText);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditMessageTest_GiveFakeEditor_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var subscriber = new User(
                subscriberId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var subscribers = new List<IUser>() { subscriber };
            var administrators = new List<IUser>() { creator };
            var channelId = Guid.NewGuid();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);
            var editedText = "EditedText";
            var fakeEditor = new User(
                Guid.NewGuid(),
                "Vasya",
                emptyChats);

            channel.EditMessage(fakeEditor, messageId, editedText);
        }

        [TestMethod]
        public void SendMessageTest_GiveTrueSender_MessageIsSend()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var subscriber = new User(
                subscriberId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>();
            var subscribers = new List<IUser>() { subscriber };
            var administrators = new List<IUser>() { creator };
            var channelId = Guid.NewGuid();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);

            channel.SendMessage(creator, message);

            CollectionAssert.Contains(messages, message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SendMessageTest_GiveFakeSender_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var subscriberId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var subscriber = new User(
                subscriberId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>();
            var subscribers = new List<IUser>() { subscriber };
            var administrators = new List<IUser>() { creator };
            var channelId = Guid.NewGuid();
            var channel = new Channel(
                channelId,
                creator,
                administrators,
                subscribers,
                messages);
            var fakeSender = new User(
                Guid.NewGuid(),
                "BadBoy",
                emptyChats);
            channel.SendMessage(fakeSender, message);
        }
    }
}
