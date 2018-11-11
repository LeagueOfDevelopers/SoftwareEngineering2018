using System;

namespace BusinessEntities
{
    public class StudiedWord
    {
        public StudiedWord(string value, int rightAnswersCount)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            RightAnswersCount = rightAnswersCount;
        }

        public string Value { get; set; }

        public int RightAnswersCount { get; private set; }

        public void IncreaseCount()
        {
            RightAnswersCount++;
        }

        public override bool Equals(object obj)
        {
            return obj is StudiedWord word &&
                   Value == word.Value &&
                   RightAnswersCount == word.RightAnswersCount;
        }
    }
}