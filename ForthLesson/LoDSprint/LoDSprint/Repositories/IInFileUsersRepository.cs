using System;

namespace LoDSprint.Repositories
{
    public interface IInFileUsersRepository
    {
        IUser LoadUser(Guid userId);
        void SaveUser(IUser user);
    }
}
