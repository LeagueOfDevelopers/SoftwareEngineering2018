using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner.Application
{
    public interface ISessionManager
    {
        IEnumerable<IPareOfWords> StartSession(Guid sessionId);
        Guid CreateSession(Guid userId);
        int SaveResults(Guid sessionId, Dictionary<Guid, string> answers);
        IEnumerable<ISession> GetSessions();
        void CloseSession(Guid id);
    }
}
