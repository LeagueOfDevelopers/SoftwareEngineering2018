using LoDSprintApi.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LoDSprintApi
{
    public class SessionModel
    {
        public SessionModel(Guid id, Guid creatorId, IEnumerable<QuestionModel> questions, List<AnswerModel> answers)
        {
            Id = id;
            CreatorId = creatorId;
            Questions = questions ?? throw new ArgumentNullException(nameof(questions));
            _answers = answers ?? throw new ArgumentNullException(nameof(answers));
            _sessionIsEnd = false;
        }

        public Guid Id { get; }

        public Guid CreatorId { get; }

        public IEnumerable<QuestionModel> Questions { get; }

        public IEnumerable<AnswerModel> Answers => _answers;

        public void AnswerTheQuestions(IEnumerable<AnswerModel> answers)
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

        private List<AnswerModel> _answers;
        private bool _sessionIsEnd;
    }
}
