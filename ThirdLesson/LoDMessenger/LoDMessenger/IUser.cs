using System;
using System.Collections.Generic;

namespace LoDMessenger
{
    public interface IUser
    {
        Guid Id { get; }

        string Name { get; }

        IEnumerable<IChat> GetChats(IUser getter);

        void SaveChat(IChat chat);
    }
}
