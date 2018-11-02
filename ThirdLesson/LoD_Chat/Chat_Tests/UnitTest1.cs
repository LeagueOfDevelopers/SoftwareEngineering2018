using System;
using System.Collections.Generic;
using LoD_Chat;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chat_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddMessageToChannel_AddedMessage()
        {
            var user1 = CreateClient();
            var channel = CreateChannel(user1);

            

        }

        private Client CreateClient()
        {
            return new Client(Guid.NewGuid(), 
                              "Masha"
                              );
        }

        private ChatRepository CreateChannel(Client creator)
        {
            ClientRepository clientRepository = new ClientRepository();
            MessagesRepository messagesRepository = new MessagesRepository();

            ChatRepository channel = new ChatRepository(new List<IChat>());
            channel.AddChat(new Channel(Guid.NewGuid(), creator, "Some Channel",clientRepository, messagesRepository));

            return channel;
        }

        //private MessagesRepository
    }
}
