using System;

namespace LoDSprintApi
{
    public class AnswerModel
    {
        public AnswerModel(QuestionModel question, bool value)
        {
            Question = question ?? throw new ArgumentNullException(nameof(question));
            Value = value;
        }

        public QuestionModel Question { get; }
        public bool Value;
    }
}
