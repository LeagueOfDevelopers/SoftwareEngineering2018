using System;
using System.Collections.Generic;

namespace LoD_Chat
{
    public class ChatRepository : IChatRepository
    {
        private readonly List<IChat> _chats;

        public ChatRepository(List<IChat> chats)
        {
            _chats = chats;
        }

        public IChat[] Chats => _chats.ToArray();

        public IChat GetChat(Guid chatId)
        {
            return TryGetChat(chatId) ?? throw new InvalidOperationException(
                $"Chat with id {chatId} not found");
        }

        public void AddChat(IChat chat)
        {
            IChat existantChat = TryGetChat(chat.Id);

            if (existantChat != null)
            {
                _chats.Remove(existantChat);
            }

            _chats.Add(chat);
        }

        private IChat TryGetChat(Guid chatId)
        {
            foreach (var chat in _chats)
            {
                if (chat.Id == chatId)
                {
                    return chat;
                }
            }

            return null;
        }
    }
}
