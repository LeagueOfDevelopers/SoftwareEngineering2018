using System;
using System.Collections.Generic;
using LoDMessenger;
using LoDMessenger.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoDMessengerTests
{
    [TestClass]
    public class GroupChatTests
    {
        [TestMethod]
        public void GetMessagesTest_GiveTrueParticipant_GetMessages()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var participantId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var participant = new User(
                participantId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator, participant };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);

            var getMessagesResult = groupChat.GetMessages(participant);

            CollectionAssert.AreEqual((List<IMessage>)getMessagesResult, messages);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetMessagesTest_GiveFakeParticipant_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var participantId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var participant = new User(
                participantId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator, participant };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);
            var fakeParticipant = new User(
                Guid.NewGuid(),
                "BadBoy",
                emptyChats);

            groupChat.GetMessages(fakeParticipant);
        }

        [TestMethod]
        public void GetParticipantsTest_GiveTrueParticipant_GetParticipants()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var participantId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var participant = new User(
                participantId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator, participant };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);

            var getParticipantsResult = groupChat.GetParticipants(participant);

            CollectionAssert.AreEqual((List<IUser>)getParticipantsResult, participants);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetParticipantsTest_GiveFakeParticipant_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var participantId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var participant = new User(
                participantId,
                "Anton",
                emptyChats);
            var message = new Message(
                    Guid.NewGuid(),
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator, participant };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);
            var fakeParticipant = new User(
                Guid.NewGuid(),
                "BadBoy",
                emptyChats);
            groupChat.GetParticipants(fakeParticipant);
        }
        [TestMethod]
        public void EditMessageTest_GiveTrueEditorAndMessageId_MessageIsEdit()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var participantId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var participant = new User(
                participantId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator, participant };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);
            var editedText = "EditedText";

            groupChat.EditMessage(creator, messageId, editedText);

            Assert.AreEqual(editedText, message.MessageText);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditMessageTest_GiveFakeEditor_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var participantId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var participant = new User(
                participantId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>() { message };
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator, participant };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);
            var editedText = "EditedText";
            var fakeEditor = new User(
                Guid.NewGuid(),
                "Vasya",
                emptyChats);

            groupChat.EditMessage(fakeEditor, messageId, editedText);
        }

        [TestMethod]
        public void SendMessageTest_GiveTrueSender_MessageIsSend()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var participantId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var participant = new User(
                participantId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>();
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator, participant };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);

            groupChat.SendMessage(participant, message);

            CollectionAssert.Contains(messages, message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SendMessageTest_GiveFakeSender_ThrowsException()
        {
            var emptyChats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var participantId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                emptyChats);
            var participant = new User(
                participantId,
                "Anton",
                emptyChats);
            var messageId = Guid.NewGuid();
            var message = new Message(
                    messageId,
                    creator,
                    "First message");
            var messages = new List<IMessage>();
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator, participant };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);
            var fakeSender = new User(
               Guid.NewGuid(),
               "BadBoy",
               emptyChats);

            groupChat.SendMessage(fakeSender, message);
        }


    }
}
