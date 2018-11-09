using System;
using BusinessEntities;

namespace BusinessServices.Interfaces
{
    public interface IStartSessionService
    {
        Session StartSession(Guid traineeUserId);
    }
}
