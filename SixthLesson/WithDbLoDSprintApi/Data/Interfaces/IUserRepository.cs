using System;
using BusinessEntities;

namespace Data.Interfaces
{
    public interface IUserRepository
    {
        TraineeUser LoadUser(Guid id);

        void SaveUser(TraineeUser user);
    }
}
