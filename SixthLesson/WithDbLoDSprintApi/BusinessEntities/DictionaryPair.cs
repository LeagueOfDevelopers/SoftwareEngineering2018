using System;

namespace BusinessEntities
{
    public class DictionaryPair
    {
        public DictionaryPair(Guid id, Word word, Translation translation)
        {
            Id = id;
            Word = word ?? throw new ArgumentNullException(nameof(word));
            Translation = translation ?? throw new ArgumentNullException(nameof(translation));
        }

        public Guid Id { get; }

        public Word Word { get; }

        public Translation Translation { get; }
    }
}
