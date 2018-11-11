using Chat;
using System;

namespace ChatTests
{
    public class StubUser : IUser
    {
        public StubUser(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public string Name => throw new NotImplementedException();

        public Message SendMessage(string text)
        {
            throw new NotImplementedException();
        }
    }
}
