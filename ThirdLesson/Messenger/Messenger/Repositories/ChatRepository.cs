using System;
using System.Collections.Generic;

namespace Messenger
{
    public class ChatRepository : IRepository<IChat>
    {
        private Dictionary<Guid, IChat> chats;

        public ChatRepository(Dictionary<Guid, IChat> chats)
        {
            this.chats = chats;
        }

        public void Create(IChat chat)
        {
            chats.Add(chat._id, chat);
        }

        public IChat Get(Guid chat_id)
        {
            if (chats.TryGetValue(chat_id, out IChat chat))
            {
                return chat;
            }
            throw new InvalidOperationException($"Chat with id {chat_id} not found");
        }


        public void Remove(Guid id)
        {
            throw new InvalidOperationException($"You can not remove chat");
        }


        public void Save(IChat chat)
        {
            if (chats.TryGetValue(chat._id, out IChat existantChat))
            {
                chats.Remove(chat._id);
            }

            chats.Add(chat._id, chat);
        }

    }
}
