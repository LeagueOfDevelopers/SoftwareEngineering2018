using System;
using LoDMessenger.Application;
using LoDMessenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LoDMessengerTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void SaveChatTest_GiveChat_ChatSaveInUserChats()
        {
            var chats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                chats);
            var messages = new List<IMessage>();
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);

            creator.SaveChat(groupChat);

            CollectionAssert.Contains(chats, groupChat);
        }

        [TestMethod]
        public void GetChatsTest_GiveCorrectGetter_GetUserChats()
        {
            var chats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                chats);
            var messages = new List<IMessage>();
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);
            creator.SaveChat(groupChat);

            var getChatsResult = creator.GetChats(creator);

            CollectionAssert.Contains((List<IChat>)getChatsResult, groupChat);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetChatsTest_GiveFakeGetter_ThrowsException()
        {
            var chats = new List<IChat>();
            var creatorId = Guid.NewGuid();
            var creator = new User(
                creatorId,
                "David",
                chats);
            var messages = new List<IMessage>();
            var administrators = new List<IUser>() { creator };
            var participants = new List<IUser> { creator };
            var groupChatId = Guid.NewGuid();
            var groupChat = new GroupChat(
                groupChatId,
                creator,
                administrators,
                participants,
                messages);
            creator.SaveChat(groupChat);
            var fakeGetter = new User(
                Guid.NewGuid(),
                "Vasya",
                new List<IChat>());

            creator.GetChats(fakeGetter);
        }
    }
}
