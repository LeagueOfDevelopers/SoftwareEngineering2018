using System;
using System.Collections.Generic;

namespace Messenger
{
    public class PrivateChatRepository : IRepository<PrivateChat>
    {
        public PrivateChatRepository(List<PrivateChat> privateChats)
        {
            _privateChats = privateChats ?? throw new ArgumentNullException(nameof(privateChats));
        }

        private List<PrivateChat> _privateChats;

        public IEnumerable<PrivateChat> Items => _privateChats;

        public void DeleteItem(PrivateChat privateChat)
        {
            _privateChats.Remove(privateChat);
        }

        public void DeleteItemById(Guid privateChatId)
        {
            _privateChats.RemoveAll(privateChat => privateChat.Id == privateChatId);
        }

        public PrivateChat GetItem(Guid privateChatId)
        {
            return TryGetPrivateChat(privateChatId) ?? throw new InvalidOperationException(
                $"Private chat with id {privateChatId} not found");
        }

        public void SaveItem(PrivateChat privateChat)
        {
            if (TryGetPrivateChat(privateChat.Id) == null)
            {
                _privateChats.Add(privateChat);
            }
        }

        public void UpdateItem(PrivateChat privateItem)
        {
            DeleteItemById(privateItem.Id);
            SaveItem(privateItem);
        }

        public void AddItem(PrivateChat item)
        {
            _privateChats.Add(item);
        }

        private PrivateChat TryGetPrivateChat(Guid privateChatId)
        {
            foreach (var privateChat in _privateChats)
            {
                if (privateChat.Id == privateChatId)
                {
                    return privateChat;
                }
            }
            return null;
        }
    }
}
