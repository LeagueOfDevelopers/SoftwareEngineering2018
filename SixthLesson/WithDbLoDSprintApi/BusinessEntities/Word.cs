using System;

namespace BusinessEntities
{
    public class Word
    {
        public Word(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            return obj is Word word &&
                   Value == word.Value;
        }
    }
}
