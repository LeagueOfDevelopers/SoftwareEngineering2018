using System;
using System.Collections.Generic;
using BusinessEntities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class InMemorySessionRepository : ISessionRepository
    {
        public InMemorySessionRepository(Dictionary<Guid, Session> sessions)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
        }

        public Session LoadSession(Guid sessionId)
        {
            return _sessions[sessionId];
        }

        public void SaveSession(Session session)
        {
            _sessions[session.Id] = session;
        }

        public void DeleteSession(Guid sessionId)
        {
            _sessions.Remove(sessionId);
        }

        public bool ContainsSession(Guid sessionId)
        {
            return _sessions
                .ContainsKey(sessionId);
        }

        private Dictionary<Guid, Session> _sessions;
    }
}

