using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leo_sprint
{
    public class SessionClient
    {
        private static Dictionary<Guid,Session> sessions = new Dictionary<Guid, Session>();
        
        public static Guid StartSession(User user,int number_of_words)
        {
            var session= user.StartSession(number_of_words);
            sessions.Add(session._id, session);
            return session._id;
        }

        public static IEnumerable<string> ShowTask(Guid session_id)
        {
            return sessions[session_id].ShowTask();
        }

        public static void GetAnswers(bool[] answers, Guid session_id)
        {
            sessions[session_id].GetAnswers(answers);
        }

        public static IEnumerable<Word> CheckAnswers(User user, Guid session_id)
        {
            return sessions[session_id].CheckAnswers(user);
        }
    }
}
