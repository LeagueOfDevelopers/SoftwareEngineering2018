using System;

namespace English.Domain
{
    public class Word : IWord
    {
        public Word(
            Guid id, 
            string body, 
            string translation,
            int countToBeLearned)
        {
            Id = id;
            Body = body ?? throw new ArgumentNullException(nameof(body));
            Translation = translation ?? throw new ArgumentNullException(nameof(translation));
            CountToBeLearned = countToBeLearned;
        }

        public Guid Id { get; private set; }

        public string Body { get; private set; }

        public string Translation { get; private set; }

        public int CountToBeLearned { get; }
    }
}
