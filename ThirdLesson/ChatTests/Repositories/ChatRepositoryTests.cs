using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System.Collections.Generic;

namespace ChatTests
{
    [TestClass]
    public class ChatRepositoryTests
    {
        [TestMethod]
        public void AddChat_ChatWillBeAdded()
        {
            StubChat chat = new StubChat(Guid.NewGuid());
            ChatRepository repa = new ChatRepository(new List<IChat>());

            repa.AddChat(chat);
            
            foreach(IChat chatInRepa in repa.Chats)
            {
                Assert.IsTrue(chatInRepa.Id == chat.Id);
            }
        }
        
    }
}
