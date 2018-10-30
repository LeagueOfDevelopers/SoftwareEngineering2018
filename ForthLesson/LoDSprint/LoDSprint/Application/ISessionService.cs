using System;

namespace LoDSprint.Application
{
    public interface ISessionService
    {
        Session StartSession(Guid traineeUserId);
        void FinishSession(Session finishedSession);
    }
}
