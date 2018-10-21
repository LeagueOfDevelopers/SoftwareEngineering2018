using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public class ChatRepository : IChatRepository
    {
        public ChatRepository(List<IChat> chats)
        {
            _chats = chats ?? throw new ArgumentNullException(nameof(chats));
        }

        public IEnumerable<IChat> Chats => _chats;

        public IChat GetChat(Guid chatId)
        {
            return TryGetChat(chatId)
                ?? throw new ArgumentException(
                    $"Chat with id {chatId} not exist");
        }

        public void SaveChat(IChat chat)
        {
            var existentChat = TryGetChat(chat.Id);
            if (existentChat != null)
                _chats.Remove(existentChat);
            _chats.Add(chat);
        }

        private IChat TryGetChat(Guid chatId)
        {
            return _chats
                .Find(chat =>
                    chat.Id == chatId);
        }

        private List<IChat> _chats;
    }
}
