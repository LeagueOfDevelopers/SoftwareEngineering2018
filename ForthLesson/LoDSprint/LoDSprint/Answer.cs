using System;

namespace LoDSprint
{
    public class Answer
    {
        public Answer(Question question, bool value)
        {
            Question = question ?? throw new ArgumentNullException(nameof(question));
            Value = value;
        }

        public Question Question { get; }
        public readonly bool Value;
    }
}
