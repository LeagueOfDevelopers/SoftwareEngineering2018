using System;
using System.Collections.Generic;

namespace LoDSprint.Repositories
{
    public class InMemorySessionsRepository
    {
        public Session LoadSession(Guid sessionId)
        {
            return _sessions[sessionId];
        }

        public void SaveSession(Session session)
        {
            _sessions[session.Id] = session;
        }

        public bool ContainsSession(Session session)
        {
            return _sessions
                .ContainsValue(session);
        }

        private Dictionary<Guid, Session> _sessions;
    }
}
