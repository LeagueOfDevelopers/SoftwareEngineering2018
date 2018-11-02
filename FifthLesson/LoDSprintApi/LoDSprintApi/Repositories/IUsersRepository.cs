using System;

namespace LoDSprintApi.Repositories
{
    public interface IUsersRepository
    {
        UserModel LoadUser(Guid userId);
        void SaveUser(UserModel user);
    }
}
