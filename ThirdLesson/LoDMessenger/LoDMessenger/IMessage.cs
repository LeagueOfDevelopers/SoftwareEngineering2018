using System;

namespace LoDMessenger
{
    public interface IMessage
    {
        Guid Id { get; }
        IUser Author { get; }
        string MessageText { get; }

        void EditText(string editedMessageText);
    }
}
