using Chat;
using System;
using System.Collections.Generic;

namespace ChatTests
{
    public class StubChat : IChat
    {
        public StubChat(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public IEnumerable<Message> Messages => throw new NotImplementedException();
        public IEnumerable<IUser> Users => throw new NotImplementedException();

        public bool AddMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(Guid id, IUser sender)
        {
            throw new NotImplementedException();
        }

        public void EditMessage(Guid id, IUser sender, string newText)
        {
            throw new NotImplementedException();
        }

        public Message FindMessageById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IUser FindUserById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
