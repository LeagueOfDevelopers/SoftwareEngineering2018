using System;

namespace English.Domain
{
    public interface IUserDatabase
    {
        void SaveUserToFile(IUser user);

        IUser LoadUserFromFile(Guid id);
    }
}
