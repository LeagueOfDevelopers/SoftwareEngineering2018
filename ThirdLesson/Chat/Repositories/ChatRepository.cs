using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat
{
    public class ChatRepository : IChatRepository
    {
        private List<IChat> chats;

        public ChatRepository(IEnumerable<IChat> chats)
        {
            this.chats = chats.ToList() ?? throw new ArgumentNullException(nameof(chats));
        }

        public ChatRepository()
        {
            chats = new List<IChat>();
        }

        public IEnumerable<IChat> Chats => chats;

        public void AddChat(IChat chat)
        {
            chats.Add(chat);
        }
    }
}
