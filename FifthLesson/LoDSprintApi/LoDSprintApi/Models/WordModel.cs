using System;
using System.Collections.Generic;

namespace LoDSprintApi
{
    public class WordModel
    {
        public WordModel(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            var word = obj as WordModel;
            return word != null &&
                   Value == word.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }

        public static bool operator ==(WordModel word1, WordModel word2)
        {
            return EqualityComparer<WordModel>.Default.Equals(word1, word2);
        }

        public static bool operator !=(WordModel word1, WordModel word2)
        {
            return !(word1 == word2);
        }
    }
}
