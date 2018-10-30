using System;
using English.Domain;

namespace English.Application
{
    public interface IUserService
    {
        Guid RegisterNewUser(string name);

        IUser LoadUser(Guid userId);
    }
}
