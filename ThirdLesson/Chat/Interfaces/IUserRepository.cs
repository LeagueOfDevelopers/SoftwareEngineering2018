using System;
using System.Collections.Generic;

namespace Chat
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Users { get; }

        void AddUser(IUser user);
        bool Contains(Guid userId);
    }
}
