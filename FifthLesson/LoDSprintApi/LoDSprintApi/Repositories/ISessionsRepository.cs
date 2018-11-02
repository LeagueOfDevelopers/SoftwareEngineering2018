using System;

namespace LoDSprintApi.Repositories
{
    public interface ISessionsRepository
    {
        SessionModel LoadSession(Guid sessionId);

        void SaveSession(SessionModel session);

        void DeleteSession(Guid sessionId);

        bool ContainsSession(Guid sessionId);
    }
}
