using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearner
{
    public interface ISessionManager
    {
        IEnumerable<SessionPareOfWords> GetExercise(Guid sessionId);
        void StartSession(Guid sessionId);
        Guid CreateSession(Guid userId);
        void SaveResults(Guid sessionId);
    }
}
