using System;
using System.Collections.Generic;

namespace LoDMessenger.Application
{
    public class User : IUser
    {
        public User(
            Guid id,
            string name,
            List<IChat> chats)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _chats = chats ?? throw new ArgumentNullException(nameof(chats));
        }
        public Guid Id { get; }

        public string Name { get; }

        public void SaveChat(IChat chat)
        {
            var existentChat = _chats.Find(currentChat => 
                currentChat.Id == chat.Id);
            if (existentChat != null)
                _chats.Remove(existentChat);
            _chats.Add(chat);
        }

        public IEnumerable<IChat> GetChats(IUser getter)
        {
            if (getter.Id == Id)
                return _chats;
            else
                throw new ArgumentException(
                    $"user with Id {getter.Id} don't have permissions to get user messages");
        }
        private List<IChat> _chats;
    }
}
