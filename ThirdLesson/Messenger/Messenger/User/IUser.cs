using System;

namespace Messenger
{
    public interface IUser
    {
        Guid Id { get; }

        string Name { get; }
    }
}
