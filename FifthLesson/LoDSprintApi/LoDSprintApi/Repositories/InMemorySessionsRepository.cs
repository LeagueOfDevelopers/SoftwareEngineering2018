using System;
using System.Collections.Generic;

namespace LoDSprintApi.Repositories
{
    public class InMemorySessionsRepository : ISessionsRepository
    {
        public InMemorySessionsRepository()
        {
            _sessions = new Dictionary<Guid, SessionModel>();
        }

        public SessionModel LoadSession(Guid sessionId)
        {
            return _sessions[sessionId];
        }

        public void SaveSession(SessionModel session)
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

        private Dictionary<Guid, SessionModel> _sessions;
    }
}
