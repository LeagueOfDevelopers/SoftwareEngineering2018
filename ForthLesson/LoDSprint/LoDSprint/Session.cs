using LoDSprint.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoDSprint
{
    public class Session
    {
        public Session(Guid id, Guid creatorId, IEnumerable<Question> questions, List<Answer> answers)
        {
            Id = id;
            CreatorId = creatorId;
            Questions = questions ?? throw new ArgumentNullException(nameof(questions));
            _answers = answers ?? throw new ArgumentNullException(nameof(answers));
            _sessionIsEnd = false;
        }

        public Guid Id { get; }

        public Guid CreatorId { get; }

        public IEnumerable<Question> Questions { get; }

        public IEnumerable<Answer> Answers => _answers;

        public void AnswerTheQuestions(IEnumerable<Answer> answers)
        {
            if (!_sessionIsEnd)
            {
                _answers = answers.ToList();
                _sessionIsEnd = true;
            }
            else
                throw new SeveralTimeAnsweringException(
                    "It's impossible to Answer the questions several times");
        }

        public override bool Equals(object obj)
        {
            var session = obj as Session;
            return session != null &&
                   Id.Equals(session.Id) &&
                   CreatorId.Equals(session.CreatorId) &&
                   EqualityComparer<IEnumerable<Question>>.Default.Equals(Questions, session.Questions);
        }

        public static bool operator ==(Session session1, Session session2)
        {
            return EqualityComparer<Session>.Default.Equals(session1, session2);
        }

        public static bool operator !=(Session session1, Session session2)
        {
            return !(session1 == session2);
        }

        private List<Answer> _answers;
        private bool _sessionIsEnd;
    }
}
