using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WithDbLoDSprintApi.Models
{
    public class StartedSessionModel
    {
        public StartedSessionModel(Guid sessionId, IEnumerable<Question> questions)
        {
            SessionId = sessionId;
            Questions = questions ?? throw new ArgumentNullException(nameof(questions));
        }

        public Guid SessionId { get; }

        public IEnumerable<Question> Questions { get; }
    }
}
