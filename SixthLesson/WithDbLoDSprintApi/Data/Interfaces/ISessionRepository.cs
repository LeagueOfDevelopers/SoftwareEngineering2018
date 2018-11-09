using System;
using BusinessEntities;

namespace Data.Interfaces
{
    public interface ISessionRepository
    {
        Session LoadSession(Guid id);

        void SaveSession(Session session);

        void DeleteSession(Guid sessionId);

        bool ContainsSession(Guid sessionId);
    }
}
