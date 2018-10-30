using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTrainer.Repositories
{
   public class SessionRepository : IRepository<Session>
   {
      private readonly List<Session> _sessions;
      
      public SessionRepository(List<Session> sessions = null)
      {
         _sessions = sessions ?? new List<Session>();
      }

      public Session Get(Guid id)
      {
         return _sessions.FirstOrDefault(session => session.SessionId == id);
      }

      public void Save(Session session)
      {
         _sessions.Add(session);
      }
   }
}