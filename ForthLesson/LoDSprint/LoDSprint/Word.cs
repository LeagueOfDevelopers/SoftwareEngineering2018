using System;
using System.Collections.Generic;

namespace LoDSprint
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
            var word = obj as Word;
            return word != null &&
                   Value == word.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }

        public static bool operator ==(Word word1, Word word2)
        {
            return EqualityComparer<Word>.Default.Equals(word1, word2);
        }

        public static bool operator !=(Word word1, Word word2)
        {
            return !(word1 == word2);
        }
    }
}
