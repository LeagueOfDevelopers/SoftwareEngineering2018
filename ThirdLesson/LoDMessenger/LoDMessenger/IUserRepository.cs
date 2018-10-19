using System;

namespace LoDMessenger
{
    public interface IUserRepository
    {
        IUser[] Users { get; }
        IUser GetUser(Guid id);
        void SaveUser(IUser user);
    }
}
