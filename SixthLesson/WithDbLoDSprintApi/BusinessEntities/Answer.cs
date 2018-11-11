using System;

namespace BusinessEntities
{
    public class Answer 
    {
        public Answer(Question question, bool value)
        {
            Question = question ?? throw new ArgumentNullException(nameof(question));
            Value = value;
        }

        public Question Question { get; }

        public bool Value { get; }
    }
}
