using System;
using System.Collections.Generic;

namespace LoDSprint
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

        public override bool Equals(object obj)
        {
            var question = obj as Question;
            return question != null &&
                   EqualityComparer<Word>.Default.Equals(Word, question.Word) &&
                   EqualityComparer<Translation>.Default.Equals(ProposedTranslation, question.ProposedTranslation);
        }

        public static bool operator ==(Question question1, Question question2)
        {
            return EqualityComparer<Question>.Default.Equals(question1, question2);
        }

        public static bool operator !=(Question question1, Question question2)
        {
            return !(question1 == question2);
        }
    }
}
