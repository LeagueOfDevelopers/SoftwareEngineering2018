using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class Session
    {
        public Session(Guid id, Guid creatorId, IEnumerable<Question> questions, IEnumerable<Answer> answers)
        {
            Id = id;
            CreatorId = creatorId;
            Questions = questions ?? throw new ArgumentNullException(nameof(questions));
            Answers = answers ?? throw new ArgumentNullException(nameof(answers));
        }

        public Guid Id { get; }

        public Guid CreatorId { get; }

        public IEnumerable<Question> Questions { get; }

        public IEnumerable<Answer> Answers { get; private set; }

        public void AnswerTheQuestions(IEnumerable<Answer> answers)
        {
            Answers = answers;
        }
    }
}
