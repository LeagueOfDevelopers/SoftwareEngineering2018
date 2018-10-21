using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDChat
{
    public class ChatRepository : IChatRepository
    {
        public ChatRepository(List<IChat> chats)
        {
            _chats = chats ?? throw new ArgumentNullException(nameof(chats));
        }
        public IEnumerable<IChat> Chats => _chats.ToList();
        public IChat GetChat(Guid idOfChat)
        {
            return TryGetChat(idOfChat) ?? 
                throw new InvalidOperationException(
                     $"Chat not found");
        }
        public void SaveChat(IChat chat)
        {
            IChat existantChat = TryGetChat(chat.IdOfChat);
            if (existantChat != null)
            {
                _chats.Remove(existantChat);
            }
            _chats.Add(chat);
        }
        private IChat TryGetChat(Guid idOfChat)
        {
            foreach (var chat in _chats)
            {
                if (chat.IdOfChat == idOfChat)
                {
                    return chat;
                }
            }
            return null;
        }
        private readonly List<IChat> _chats;

    }
}
