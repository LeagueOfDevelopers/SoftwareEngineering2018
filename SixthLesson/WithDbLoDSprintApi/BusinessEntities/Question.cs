using System;

namespace BusinessEntities
{
    public class Question
    {
        public Question(Word word, Translation proposedTranslation)
        {
            Word = word ?? throw new ArgumentNullException(nameof(word));
            ProposedTranslation = proposedTranslation ?? throw new ArgumentNullException(nameof(proposedTranslation));
        }

        public Word Word { get; }

        public Translation ProposedTranslation { get; }
    }
}
