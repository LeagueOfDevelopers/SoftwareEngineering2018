using System;

namespace Chat
{
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; }

        Message SendMessage(string text);
    }
}
